using UnityEngine;
using UnityEngine.EventSystems;

namespace OpenTibiaUnity.Modules.BuddyList
{
    [DisallowMultipleComponent]
    public class BuddyListAddWidget : UI.Legacy.PopUpBase
    {
        [SerializeField]
        private TMPro.TMP_InputField _name = null;

        protected override void Awake() {
            base.Awake();

            AddButton(UI.Legacy.PopUpButtonMask.Ok, OnOkButtonClick);
            AddButton(UI.Legacy.PopUpButtonMask.Cancel);
        }
        private void OnOkButtonClick() {
            if (!string.IsNullOrEmpty(_name.text)) {
                OpenTibiaUnity.ProtocolGame.SendAddBuddy(_name.text);
            }
        }

        public override void Select() {
            _name.Select();
        }

        public override void Show() {
            base.Show();
            _name.text = "";
        }
    }
}
