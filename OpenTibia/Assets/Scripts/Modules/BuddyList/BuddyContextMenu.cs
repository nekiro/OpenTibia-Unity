using OpenTibiaUnity.Core.BuddyList;
using OpenTibiaUnity.Core.Chat;
using OpenTibiaUnity.Core.Input.GameAction;
using UnityEngine;
using TR = OpenTibiaUnity.TextResources;

namespace OpenTibiaUnity.Modules.BuddyList
{
    public class BuddyContextMenu : UI.Legacy.ContextMenuBase
    {
        private Buddy _buddy;
        private BuddyListWidget _widget = OpenTibiaUnity.GameManager.GetModule<BuddyListWidget>();

        public void Set(Buddy buddy) {
            _buddy = buddy;
        }

        public override void InitialiseOptions() {
            if (_buddy != null) { 
                CreateTextItem(string.Format(TR.CTX_VIP_EDIT_VIP, _buddy.Name), () => {
                    _widget.ShowEditBuddyWidget(_buddy);
                });

                CreateTextItem(string.Format(TR.CTX_VIP_REMOVE_VIP, _buddy.Name), () => {
                    OpenTibiaUnity.BuddyStorage.RemoveBuddy(_buddy);
                    OpenTibiaUnity.ProtocolGame.SendRemoveBuddy(_buddy.Id);
                });

                if (_buddy.Status == BuddyStatus.Online) {
                    CreateTextItem(string.Format(TR.CTX_VIEW_PRIVATE_MESSAGE, _buddy.Name), () => {
                        // todo, notify about expected channel :)
                        new PrivateChatActionImpl(PrivateChatActionType.OpenMessageChannel, PrivateChatActionImpl.ChatChannelNoChannel, _buddy.Name).Perform();
                    });
                }
             }

            CreateTextItem(TR.CTX_VIP_ADD_VIP, () => {
                _widget.ShowAddBuddyWidget();
            });

            CreateSeparatorItem();

            CreateTextItem(TR.CTX_VIP_SORT_BY_NAME, () => {
                _widget.ChangeSortType(FilterType.Name);
            });

            CreateTextItem(TR.CTX_VIP_SORT_BY_TYPE, "TODO", () => {
                _widget.ChangeSortType(FilterType.Type);
            });

            CreateTextItem(TR.CTX_VIP_SORT_BY_STATUS, () => {
                _widget.ChangeSortType(FilterType.Status);
            });

            CreateTextItem(TR.CTX_VIP_HIDE_OFFLINE, () => {
                _widget.SwitchHideOffline();
            });

            if (OpenTibiaUnity.GameManager.ClientVersion >= 1100) {
                CreateTextItem(TR.CTX_VIP_SHOW_GROUPS, "TODO", () => {
                    // TODO
                });
            }

            if (_buddy != null) {
                CreateSeparatorItem();

                CreateTextItem(TR.CTX_PLAYER_REPORT_NAME, "TODO", () => {
                    // TODO
                });

                CreateSeparatorItem();

                CreateTextItem(TR.CTX_VIEW_COPY_NAME, () => {
                    GUIUtility.systemCopyBuffer = _buddy.Name;
                });
            }
        }
    }
}
