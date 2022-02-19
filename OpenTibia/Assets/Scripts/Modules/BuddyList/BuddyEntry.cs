using OpenTibiaUnity.Core.BuddyList;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OpenTibiaUnity.Modules.BuddyList
{
    public class BuddyEntry : Core.Components.Base.AbstractComponent, IPointerEnterHandler, IPointerExitHandler
    {
        // serialized field
        [SerializeField]
        private TMPro.TextMeshProUGUI _nameLabel = null;
        [SerializeField]
        private Image _image = null;
        [SerializeField]
        private Sprite[] _textures = null;

        // properties
        private Buddy _buddy = null;
        public Buddy buddy {
            get => _buddy;
            set {
                _buddy = value;
                UpdateInfo();
            }
        }

        public new string name { get { return $"Buddy_{_buddy.Name}"; } internal set { } }

        private void UpdateInfo() {
            _nameLabel.text = _buddy.Name;
            UpdateStatus();
            UpdateIcon();
        }

        public void UpdateStatus() {
            Color color = Core.Colors.Default;

            switch (_buddy.Status) {
                case BuddyStatus.Offline: color = Core.Colors.Red; break;
                case BuddyStatus.Pending: color = Core.Colors.Orange; break;
                case BuddyStatus.Training: color = Core.Colors.Purple; break;
                case BuddyStatus.Online: color = Core.Colors.Green; break;
                default: break;
            }

            _nameLabel.color = color;
        }

        public void UpdateIcon() {
            _image.sprite = _textures[_buddy.Icon];
        }

        public void UpdateDesc(string desc) {
            _buddy.Desc = desc;
        }

        public void OnPointerEnter(PointerEventData eventData) {
            OpenTibiaUnity.GameManager.GetModule<BuddyListWidget>().activeWidget = this;
        }

        public void OnPointerExit(PointerEventData eventData) {
           OpenTibiaUnity.GameManager.GetModule<BuddyListWidget>().activeWidget = null;
        }
    }
}
