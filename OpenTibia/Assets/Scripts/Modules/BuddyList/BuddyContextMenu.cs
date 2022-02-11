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

        public void Set(Buddy buddy) {
            _buddy = buddy;
        }

        public override void InitialiseOptions() {
            var gameManager = OpenTibiaUnity.GameManager;
            var creatureStorage = OpenTibiaUnity.CreatureStorage;
            var chatStorage = OpenTibiaUnity.ChatStorage;

            CreateTextItem(string.Format(TR.CTX_VIEW_PRIVATE_MESSAGE, _buddy.Name), () => {
                // todo, notify about expected channel :)
                new PrivateChatActionImpl(PrivateChatActionType.OpenMessageChannel, PrivateChatActionImpl.ChatChannelNoChannel, _buddy.Name).Perform();
            });

            CreateTextItem(TR.CTX_VIP_ADD_VIP, "TODO", () => {
                // TODO
            });

            CreateTextItem(string.Format(TR.CTX_VIP_EDIT_VIP, _buddy.Name), "TODO", () => {
               // TODO
            });

            CreateTextItem(string.Format(TR.CTX_VIP_REMOVE_VIP, _buddy.Name), "TODO", () => {
                //TODO
            });

            CreateSeparatorItem();

            CreateTextItem(TR.CTX_VIEW_COPY_NAME, () => {
                GUIUtility.systemCopyBuffer = _buddy.Name;
            });
        }
    }
}
