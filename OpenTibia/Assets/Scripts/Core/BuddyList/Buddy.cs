using System.Collections.Generic;
using UnityEngine.Events;

namespace OpenTibiaUnity.Core.BuddyList
{
    public class Buddy
    {
        public class BuddyStatusChangeEvent : UnityEvent<Buddy, BuddyStatus> { };

        public static BuddyStatusChangeEvent onStatusChange = new BuddyStatusChangeEvent(); // status

        public uint Id { get; private set; }
        public string Name { get; private set; }
        public string Desc { get; private set; }
        public uint Icon { get; private set; }
        public bool NotifyLogin { get; private set; }
        public BuddyStatus Status { get; private set; }
        public List<byte> Groups {get; private set; } // TODO: actually support these

        public Buddy(uint id, string name, string desc, uint icon, bool notify, BuddyStatus status, List<byte> groups = null)
        {
            Id = id;
            Name = name;
            Desc = desc;
            Icon = icon;
            NotifyLogin = notify;
            Status = status;

            if (groups != null) {
                Groups = groups;
            }
        }

        public void SetStatus(BuddyStatus status) {
            Status = status;
            onStatusChange.Invoke(this, status);
        }
    }
}
