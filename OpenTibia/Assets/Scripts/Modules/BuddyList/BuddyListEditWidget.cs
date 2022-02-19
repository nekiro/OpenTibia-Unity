using OpenTibiaUnity.Core.BuddyList;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace OpenTibiaUnity.Modules.BuddyList
{
    [DisallowMultipleComponent]
    public class BuddyListEditWidget : UI.Legacy.PopUpBase
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI _nameLabel = null;
        [SerializeField]
        private TMPro.TMP_InputField _desc = null;
        [SerializeField]
        private GridLayoutGroup _iconsHolder = null;

        // fields
        private Buddy _buddy = null;
        private Toggle _selectedIcon = null;
        private Dictionary<uint, Toggle> _icons = new Dictionary<uint, Toggle>();

        public Buddy Buddy {
            set {
                _buddy = value;
                _nameLabel.text = _buddy.Name;
                _desc.text = _buddy.Desc;
                ToggleIcon(_buddy.Icon);
            }
            get => _buddy;
        }

        protected override void Awake() {
            base.Awake();

            AddButton(UI.Legacy.PopUpButtonMask.Ok, OnOkButtonClick);
            AddButton(UI.Legacy.PopUpButtonMask.Cancel);

            // events
            for (uint i = 0; i < 11; i++) {
                createToggle(i);
            }
        }

        private Toggle createToggle(uint i) {
            // instantiate toggle
            var icon = Instantiate(OpenTibiaUnity.GameManager.TogglePrefab, _iconsHolder.transform);

            // set icon sprite
            var label = icon.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            label.text = $"   <sprite=\"VipIcons\" index={i}>";
            label.fontSize = 11;
            label.enableWordWrapping = false;

            // hook event
            icon.onValueChanged.AddListener((bool _) => ToggleIcon(i));
            _icons.Add(i, icon);
            return icon;
        }

        private void OnOkButtonClick() {
            if (_buddy != null) {
                OpenTibiaUnity.ProtocolGame.SendEditBuddy(_buddy.Id, _desc.text, _buddy.Icon, _buddy.NotifyLogin, _buddy.Groups);
                OpenTibiaUnity.BuddyStorage.SetBuddyIcon(_buddy.Id, _buddy.Icon);
                OpenTibiaUnity.BuddyStorage.SetBuddyDesc(_buddy.Id, _desc.text);
            }
        }

        private void ToggleIcon(uint icon) {
            if (_selectedIcon) {
                _selectedIcon.SetIsOnWithoutNotify(false);
                _selectedIcon.interactable = true;
            }

            var toggle = _icons[icon];
            if (toggle != null) {
                toggle.SetIsOnWithoutNotify(true);
                toggle.interactable = false;
                _selectedIcon = toggle;
                _buddy.Icon = icon;
            }
        }
    }
}
