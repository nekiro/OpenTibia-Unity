using OpenTibiaUnity.Core.BuddyList;
using System.Collections.Generic;
using UnityEngine;

namespace OpenTibiaUnity.Core.Communication.Game
{
    public partial class ProtocolGame
    {
        private void ParseBuddyAdd(Internal.CommunicationStream message) {
            uint creatureId = message.ReadUnsignedInt();
            string name = message.ReadString();
            string desc = string.Empty;
            uint icon = 0;
            bool notifyLogin = false;
            if (OpenTibiaUnity.GameManager.GetFeature(GameFeature.GameAdditionalVipInfo)) {
                desc = message.ReadString();
                icon = message.ReadUnsignedInt();
                notifyLogin = message.ReadBoolean();
            }

            BuddyStatus status = (BuddyStatus)message.ReadUnsignedByte();
            List<byte> groups = null;

            if (OpenTibiaUnity.GameManager.GetFeature(GameFeature.GameBuddyGroups)) {
                int count = message.ReadUnsignedByte();
                groups = new List<byte>(count);
                for (int i = 0; i < count; i++) {
                    groups.Add(message.ReadUnsignedByte());
                }
            }

            OpenTibiaUnity.BuddyStorage.AddBuddy(creatureId, new Buddy(creatureId, name, desc, icon, notifyLogin, status, groups));
        }

        private void ParseBuddyState(Internal.CommunicationStream message) {
            uint creatureId = message.ReadUnsignedInt();
            BuddyStatus state = BuddyStatus.Online;
            if (OpenTibiaUnity.GameManager.GetFeature(GameFeature.GameLoginPending))
                state = (BuddyStatus)message.ReadUnsignedByte();

            OpenTibiaUnity.BuddyStorage.SetBuddyState(creatureId, state);
        }

        private void ParseBuddyLogout(Internal.CommunicationStream message) {
            uint creatureId = message.ReadUnsignedInt();
        }

        private void ParseBuddyGroupData(Internal.CommunicationStream message) {
            int groups = message.ReadUnsignedByte();
            for (int i = 0; i < groups; i++) {
                message.ReadUnsignedByte(); // id
                message.ReadString(); // name
                message.ReadUnsignedByte(); // idk
            }

            message.ReadUnsignedByte(); // premium/free iirc (since free players are allowed only for 5 groups)
        }
    }
}
