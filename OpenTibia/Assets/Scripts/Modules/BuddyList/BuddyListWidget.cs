using OpenTibiaUnity.Core.Appearances;
using OpenTibiaUnity.Core.BuddyList;
using OpenTibiaUnity.Core.Game;
using System.Collections.Generic;
using UnityEngine;

namespace OpenTibiaUnity.Modules.BuddyList
{
    public class BuddyListWidget : UI.Legacy.SidebarWidget, IUseWidget, IMoveWidget
    {
        // serialized fields
        [SerializeField]
        private RectTransform _buddyList = null;

        // non-serialized fields
        [System.NonSerialized]
        public BuddyEntry activeWidget = null;

        // fields
        private Dictionary<uint, BuddyEntry> buddies = new Dictionary<uint, BuddyEntry>();

        protected override void Awake() {
            base.Awake();

            // setup events
            Buddy.onStatusChange.AddListener(OnStatusChange);
        }

        protected override void Start() {
            base.Start();

            CheckBuddies();
        }

        protected override void OnEnable() {
            base.OnEnable();

            OpenTibiaUnity.InputHandler.AddMouseUpListener(Core.Utils.EventImplPriority.Default, OnMouseUp);
        }

        protected override void OnDisable() {
            base.OnDisable();

            if (OpenTibiaUnity.InputHandler != null)
                OpenTibiaUnity.InputHandler.RemoveMouseUpListener(OnMouseUp);
        }

        public void OnMouseUp(Event e, MouseButton mouseButton, bool repeat) {
            if (InternalStartMouseAction(e.mousePosition, mouseButton, true, false))
                e.Use();
        }

        protected override void OnClientVersionChange(int _, int newVersion) {
            //bool isTibia11 = newVersion >= 1100;
        }

        public int GetTopObjectUnderPoint(Vector3 mousePosition, out ObjectInstance @object)
        {
            throw new System.NotImplementedException();
        }

        public int GetUseObjectUnderPoint(Vector3 mousePosition, out ObjectInstance @object)
        {
            throw new System.NotImplementedException();
        }

        public int GetMultiUseObjectUnderPoint(Vector3 mousePosition, out ObjectInstance @object)
        {
            throw new System.NotImplementedException();
        }

        public int GetMoveObjectUnderPoint(Vector3 mousePosition, out ObjectInstance @object)
        {
            throw new System.NotImplementedException();
        }

        private void CheckBuddies() {
            if (!OpenTibiaUnity.GameManager.IsGameRunning)
                return;

            var buddies = OpenTibiaUnity.BuddyStorage.GetBuddies();
            foreach (var pair in buddies)
                AddBuddy(pair.Value);
        }

        private void OnStatusChange(Buddy buddy, BuddyStatus status) {
            if (buddies.TryGetValue(buddy.Id, out BuddyEntry buddyEntry)) {
                buddyEntry.UpdateStatus();
            } else {
                throw new System.Exception("BuddyStorage.SetBuddyState: Unknown buddy id: " + buddy.Id);
            }
        }

        private void AddBuddy(Buddy buddy) {
            if (!buddies.TryGetValue(buddy.Id, out BuddyEntry buddyEntry)) {
                buddyEntry = Instantiate(ModulesManager.Instance.BuddyEntryPrefab, _buddyList);
                buddyEntry.transform.SetSiblingIndex(_buddyList.childCount);
                buddyEntry.buddy = buddy;

                buddies.Add(buddy.Id, buddyEntry);
            } else {
                buddyEntry.name = $"Buddy_{buddy.Name}";
                buddyEntry.buddy = buddy;
            }
        }

        private bool InternalStartMouseAction(Vector3 mousePosition, MouseButton mouseButton, bool applyAction = false, bool updateCursor = false)
        {
            var gameManager = OpenTibiaUnity.GameManager;
            if (!activeWidget || !gameManager.GameCanvas.gameObject.activeSelf || gameManager.GamePanelBlocker.gameObject.activeSelf)
                return false;

            var eventModifiers = OpenTibiaUnity.InputHandler.GetRawEventModifiers();
            var action = DetermineAction(mousePosition, mouseButton, eventModifiers, applyAction, updateCursor);
            return action != AppearanceActions.None;
        }

        public AppearanceActions DetermineAction(Vector3 mousePosition, MouseButton mouseButton, EventModifiers eventModifiers, bool applyAction = false, bool updateCursor = false)
        {
            if (!activeWidget)
                return AppearanceActions.None;

            var inputHandler = OpenTibiaUnity.InputHandler;
            if (inputHandler.IsMouseButtonDragged(MouseButton.Left) || inputHandler.IsMouseButtonDragged(MouseButton.Right))
                return AppearanceActions.None;

            var action = AppearanceActions.None;
            var optionStorage = OpenTibiaUnity.OptionStorage;

            updateCursor &= OpenTibiaUnity.GameManager.ClientVersion >= 1100;

            switch (optionStorage.MousePreset) {
                case MousePresets.Classic: {
                        if (mouseButton == MouseButton.Left) {
                            if (eventModifiers == EventModifiers.Control)
                                action = AppearanceActions.ContextMenu;
                        } else if (mouseButton == MouseButton.Right) {
                            if (eventModifiers == EventModifiers.None || eventModifiers == EventModifiers.Control)
                                action = AppearanceActions.ContextMenu;
                        }

                        break;
                    }

                case MousePresets.Regular: {
                        // TODO
                        break;
                    }

                case MousePresets.LeftSmartClick: {
                        // TODO
                        break;
                    }
            }

            if (updateCursor)
                OpenTibiaUnity.GameManager.CursorController.SetCursorState(action, CursorPriority.Medium);

            if (applyAction) {
                switch (action) {
                    case AppearanceActions.ContextMenu:
                        CreateBuddyContextMenu(activeWidget.buddy).Display(mousePosition);
                        break;
                }
            }

            return action;
        }

        private BuddyContextMenu CreateBuddyContextMenu(Buddy buddy)
        {
            var gameManager = OpenTibiaUnity.GameManager;
            var canvas = gameManager.ActiveCanvas;
            var gameObject = Instantiate(gameManager.ContextMenuBasePrefab, canvas.transform);

            var channelMessageContextMenu = gameObject.AddComponent<BuddyContextMenu>();
            channelMessageContextMenu.Set(buddy);
            return channelMessageContextMenu;
        }

    }
}
