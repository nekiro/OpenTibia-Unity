using UnityEngine;

using UnityUI = UnityEngine.UI;

namespace OpenTibiaUnity.Modules.ToggleButtons
{
    public class ToggleButtonsWidget : UI.Legacy.SidebarWidget
    {
        // serialized fields
        [SerializeField]
        private RectTransform _rightPanel = null;
        [SerializeField]
        private RectTransform _leftPanel = null;
        [SerializeField]
        private UI.Legacy.Button _storeButton = null;
        [SerializeField]
        private UI.Legacy.Toggle _dockToggle = null;
        [SerializeField]
        private UI.Legacy.Toggle _skillsToggle = null;
        [SerializeField]
        private UI.Legacy.Toggle _battleToggle = null;
        [SerializeField]
        private UI.Legacy.Toggle _buddyToggle = null;
        [SerializeField]
        private UI.Legacy.Button _questlogButton = null;
        [SerializeField]
        private UI.Legacy.Button _rewardWallButton = null;
        [SerializeField]
        private UI.Legacy.Toggle _spellListToggle = null;
        [SerializeField]
        private UI.Legacy.Toggle _unjustFragsToggle = null;
        [SerializeField]
        private UI.Legacy.Toggle _preyToggle = null;
        [SerializeField]
        private UI.Legacy.Button _optionsButton = null;
        [SerializeField]
        private UI.Legacy.Button _logoutButton = null;
        [SerializeField]
        private UnityUI.Toggle _analyticsToggle = null;

        // fields
        private bool _handlingSkills = false;
        private bool _handlingBattle = false;
        private bool _handlingBuddy = false;

        protected UI.Legacy.MessageWidget _messageWidget;

        // properties
        protected Inventory.InventoryWidget inventoryWindow {
            get => OpenTibiaUnity.GameManager.GetModule<Inventory.InventoryWidget>();
        }

        protected override void Awake() {
            base.Awake();

            _skillsToggle.onValueChanged.AddListener(OnSkillsToggleValueChanged);
            _battleToggle.onValueChanged.AddListener(OnBattleToggleValueChanged);
            _buddyToggle.onValueChanged.AddListener(OnBuddyToggleValueChanged);

            _dockToggle.isOn = true;
            _dockToggle.onValueChanged.AddListener(OnRestToggleValueChanged);

            _questlogButton.onClick.AddListener(OnQuestlogButtonClick);
            _optionsButton.onClick.AddListener(OnOptionsButtonClick);
            _logoutButton.onClick.AddListener(OnLogoutButtonClick);
            _storeButton.onClick.AddListener(OnStoreButtonClick);
        }

        protected override void OnClientVersionChange(int _, int newVersion) {
            gameObject.SetActive(newVersion >= 1000);
            if (newVersion < 1000)
                return;

            bool isTibia11 = newVersion >= 1100;
            _spellListToggle.gameObject.SetActive(isTibia11);
            _unjustFragsToggle.gameObject.SetActive(OpenTibiaUnity.GameManager.GetFeature(GameFeature.GameUnjustifiedPoints));
            _preyToggle.gameObject.SetActive(OpenTibiaUnity.GameManager.GetFeature(GameFeature.GamePrey));
            _rewardWallButton.gameObject.SetActive(OpenTibiaUnity.GameManager.GetFeature(GameFeature.GameRewardWall));
            _analyticsToggle.gameObject.SetActive(OpenTibiaUnity.GameManager.GetFeature(GameFeature.GameAnalytics));
            _dockToggle.gameObject.SetActive(isTibia11);
            _storeButton.gameObject.SetActive(isTibia11);

            int yExtention = 0;
            if (isTibia11) {
                yExtention += 22;
                layoutElement.preferredHeight = _dockToggle.isOn ? 64 : 20;
            } else {
                layoutElement.preferredHeight = 20;
            }

            _leftPanel.anchoredPosition = new Vector2(5, -yExtention);
            _rightPanel.anchoredPosition = new Vector2(-5, -yExtention);
        }

        public void OnSkillsToggleValueChanged(bool value) {
            if (_handlingSkills)
                return;

            try {
                _handlingSkills = true;
                _skillsToggle.isOn = value;
                inventoryWindow.skillsToggle.isOn = value;

                ToggleSidebarWidgetWithPrefab(ModulesManager.Instance.SkillsWidgetPrefab, value);
            } catch (System.Exception) {
            } finally {
                _handlingSkills = false;
            }
        }

        public void OnBattleToggleValueChanged(bool value) {
            if (_handlingBattle)
                return;

            try {
                _handlingBattle = true;
                _battleToggle.isOn = value;
                inventoryWindow.battleToggle.isOn = value;

                ToggleSidebarWidgetWithPrefab(ModulesManager.Instance.BattleWidgetPrefab, value);
            } catch (System.Exception) {

            } finally {
                _handlingBattle = false;
            }
        }

        public void OnBuddyToggleValueChanged(bool value) {
            if (_handlingBuddy)
                return;

            try {
                _handlingBuddy = true;
                _buddyToggle.isOn = value;
                inventoryWindow.buddyToggle.isOn = value;

                ToggleSidebarWidgetWithPrefab(ModulesManager.Instance.BuddyListWidgetPrefab, value);
            } catch (System.Exception) {

            } finally {
                _handlingBuddy = false;
            }
        }

        private void OnRestToggleValueChanged(bool value) {
            if (OpenTibiaUnity.GameManager.ClientVersion < 1100)
                return;

            _rightPanel.gameObject.SetActive(value);
            _leftPanel.gameObject.SetActive(value);
            layoutElement.preferredHeight = value ? 64 : 20;
        }

        public void OnQuestlogButtonClick() {

        }

        public void OnOptionsButtonClick() {
            if (OpenTibiaUnity.GameManager.ClientVersion < 1100)
                ModulesManager.Instance.LegacyOptionsWidget.Show();
        }

        public void OnHelpButtonClicked() {

        }

        public void OnLogoutButtonClick() {
            if (_messageWidget != null)
                _messageWidget.Hide();

            _messageWidget = UI.Legacy.MessageWidget.CreateYesNoPopUp(transform.parent, "Warning", "Are you sure you want to leave Tibia?", OnPopupYesClick, OnPopupNoClick);
            _messageWidget.maxSize = new Vector2(500, 250);
        }

        protected void OnPopupYesClick() {
            _messageWidget = null;

            var gameManager = OpenTibiaUnity.GameManager;
            if (gameManager.IsGameRunning) {
                var protocolGame = OpenTibiaUnity.ProtocolGame;
                protocolGame.Disconnect();
            }
        }

        protected void OnPopupNoClick() {
            _messageWidget = null;
        }

        public void OnStoreButtonClick() {
            var gameManager = OpenTibiaUnity.GameManager;
            if (gameManager.IsGameRunning) {
                var protocolGame = OpenTibiaUnity.ProtocolGame;
                protocolGame.SendOpenStore();
                if (gameManager.ClientVersion >= 1180) {
                    var openParamaters = new Core.Store.StoreOpenParameters(StoreOpenParameterAction.Invalid, null);
                    protocolGame.SendRequestStoreOffers(openParamaters);
                }
            }
        }

        private void ToggleSidebarWidgetWithPrefab<T>(T prefab, bool value) where T : UI.Legacy.SidebarWidget {
            var gameManager = OpenTibiaUnity.GameManager;
            var sidebarWidget = gameManager.GetModule<T>();
            if (value) {
                if (!sidebarWidget)
                    gameManager.GetModule<GameWindow.GameInterface>().CreateSidebarWidget(prefab);
            } else {
                if (sidebarWidget)
                    Destroy(sidebarWidget.gameObject);
            }
        }
    }
}
