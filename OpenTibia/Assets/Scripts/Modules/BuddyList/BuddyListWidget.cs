using OpenTibiaUnity.Core.Appearances;
using OpenTibiaUnity.Core.BuddyList;
using OpenTibiaUnity.Core.Game;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OpenTibiaUnity.Modules.BuddyList
{
    public enum FilterType {
        None = 0,
        Name = 1,
        Type = 2,
        Status = 3,
    }

    public class BuddyListWidget : UI.Legacy.SidebarWidget
    {
        // serialized fields
        [SerializeField]
        private RectTransform _buddyList = null;

        // non-serialized fields
        [System.NonSerialized]
        public BuddyEntry activeWidget = null;
        [System.NonSerialized]
        public FilterType filter = FilterType.None;
        [System.NonSerialized]
        private bool _hideOffline = false;

        // fields
        private List<BuddyEntry> _buddies = new List<BuddyEntry>();
        private BuddyListAddWidget _addBuddyWidget = null;
        private BuddyListEditWidget _editBuddyWidget = null;

        protected override void Awake() {
            base.Awake();

            // events
            Buddy.onStatusChange.AddListener(OnStatusChange);
            Buddy.onIconChange.AddListener(OnIconChange);
            Buddy.onDescChange.AddListener(OnDescChange);
            Buddy.onRemove.AddListener(OnRemove);
            Buddy.onAdd.AddListener(OnAdd);
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

            activeWidget = null;
        }

        public void OnMouseUp(Event e, MouseButton mouseButton, bool repeat) {
            if (InternalStartMouseAction(e.mousePosition, mouseButton, true, false))
                e.Use();
        }

        protected override void OnClientVersionChange(int _, int newVersion) {
            //bool isTibia11 = newVersion >= 1100;
        }

        private void CheckBuddies() {
            if (!OpenTibiaUnity.GameManager.IsGameRunning)
                return;

            RemoveAllBuddies();

            var buddies = OpenTibiaUnity.BuddyStorage.GetBuddies();
            foreach (var pair in buddies)
                AddBuddy(pair.Value);
        }

        private void AddBuddy(Buddy buddy) {
            var buddyEntry = _buddies.SingleOrDefault(b => b.buddy == buddy);
            if (buddyEntry == null) {
                buddyEntry = Instantiate(ModulesManager.Instance.BuddyEntryPrefab, _buddyList);
                buddyEntry.transform.SetSiblingIndex(_buddyList.childCount);
                buddyEntry.buddy = buddy;
                _buddies.Add(buddyEntry);
            } else {
                buddyEntry.buddy = buddy;
            }
        }

        private bool InternalStartMouseAction(Vector3 mousePosition, MouseButton mouseButton, bool applyAction = false, bool updateCursor = false) {
            var gameManager = OpenTibiaUnity.GameManager;
            if (!_mouseCursorOverRenderer || !gameManager.GameCanvas.gameObject.activeSelf || gameManager.GamePanelBlocker.gameObject.activeSelf)
                return false;

            var eventModifiers = OpenTibiaUnity.InputHandler.GetRawEventModifiers();
            var action = DetermineAction(mousePosition, mouseButton, eventModifiers, applyAction, updateCursor);
            return action != AppearanceActions.None;
        }

        public AppearanceActions DetermineAction(Vector3 mousePosition, MouseButton mouseButton, EventModifiers eventModifiers, bool applyAction = false, bool updateCursor = false) {
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
                        CreateBuddyContextMenu(activeWidget?.buddy).Display(mousePosition);
                        break;
                }
            }

            return action;
        }

        private BuddyContextMenu CreateBuddyContextMenu(Buddy buddy) {
            var gameManager = OpenTibiaUnity.GameManager;
            var canvas = gameManager.ActiveCanvas;
            var gameObject = Instantiate(gameManager.ContextMenuBasePrefab, canvas.transform);

            var channelMessageContextMenu = gameObject.AddComponent<BuddyContextMenu>();
            channelMessageContextMenu.Set(buddy);
            return channelMessageContextMenu;
        }

        private void RemoveAllBuddies() {
            foreach (var buddyEntry in _buddies)
                Destroy(buddyEntry);

            _buddies.Clear();
        }

        public void ChangeSortType(FilterType type) {
            switch (type) {
                case FilterType.Name: {
                    _buddies.Sort((a, b) => a.buddy.Name.CompareTo(b.buddy.Name));
                    break;
                }

                case FilterType.Type: {
                    // TODO
                    break;
                }

                case FilterType.Status: {
                    _buddies = _buddies
                        .GroupBy(u => u.buddy.Status)
                        .SelectMany(grp => grp.ToList())
                        .ToList();
                    break;
                }
            }

            // apply sort
            _buddyList.DetachChildren();

            foreach (BuddyEntry buddy in _buddies) {
                buddy.transform.SetParent(_buddyList);
            }
        }

        public void SwitchHideOffline() {
            _hideOffline = !_hideOffline;

            _buddyList.DetachChildren();

            foreach (BuddyEntry buddyEntry in _buddies) {
                if (!_hideOffline || buddyEntry.buddy.Status != BuddyStatus.Offline) {
                    buddyEntry.transform.SetParent(_buddyList);
                }
            }
        }

        public void ShowAddBuddyWidget() {
            if (!_addBuddyWidget) {
                _addBuddyWidget = Instantiate(ModulesManager.Instance.BuddyListAddWidget);
            }

            _addBuddyWidget.Show();
        }

        public void ShowEditBuddyWidget(Buddy buddy) {
            if (!_editBuddyWidget) {
                _editBuddyWidget = Instantiate(ModulesManager.Instance.BuddyListEditWidget);
            }

            _editBuddyWidget.Buddy = buddy;
            _editBuddyWidget.Show();
        }

        // event handlers
        private void OnStatusChange(Buddy buddy, BuddyStatus status) {
            var buddyEntry = _buddies.SingleOrDefault(b => b.buddy == buddy);
            if (buddyEntry != null) {
                buddyEntry.UpdateStatus();
            } else {
                throw new System.Exception("BuddyStorage.SetBuddyState: Unknown buddy id: " + buddy.Id);
            }
        }

        private void OnIconChange(Buddy buddy, uint icon) {
            var buddyEntry = _buddies.SingleOrDefault(b => b.buddy == buddy);
            if (buddyEntry != null) {
                buddyEntry.UpdateIcon();
            } else {
                throw new System.Exception("BuddyStorage.SetBuddyIcon: Unknown buddy id: " + buddy.Id);
            }
        }

        private void OnDescChange(Buddy buddy, string desc) {
            var buddyEntry = _buddies.SingleOrDefault(b => b.buddy == buddy);
            if (buddyEntry != null) {
                buddyEntry.UpdateDesc(desc);
            } else {
                throw new System.Exception("BuddyStorage.setBuddyDesc: Unknown buddy id: " + buddy.Id);
            }
        }

        private void OnAdd(Buddy buddy) {
            AddBuddy(buddy);
        }

        private void OnRemove(Buddy buddy) {
            var buddyEntry = _buddies.SingleOrDefault(b => b.buddy == buddy);
            if (buddyEntry != null) {
                buddyEntry.transform.SetParent(null);
                Destroy(buddyEntry);
                _buddies.Remove(buddyEntry);
            }
        }
    }
}
