using System.Collections.Generic;
using UnityEngine.Events;

namespace OpenTibiaUnity.Core.BuddyList
{
    public class Buddy
    {
        public class BuddyStatusChangeEvent : UnityEvent<Buddy, BuddyStatus> { };
        public class BuddyIconChangeEvent : UnityEvent<Buddy, uint> { };
        public class BuddyDescChangeEvent : UnityEvent<Buddy, string> { };
        public class BuddyRemoveEvent : UnityEvent<Buddy> { };
        public class BuddyAddEvent : UnityEvent<Buddy> { };

        public static BuddyStatusChangeEvent onStatusChange = new BuddyStatusChangeEvent(); // status
        public static BuddyIconChangeEvent onIconChange = new BuddyIconChangeEvent(); // icon
        public static BuddyDescChangeEvent onDescChange = new BuddyDescChangeEvent(); // desc
        public static BuddyRemoveEvent onRemove = new BuddyRemoveEvent(); // remove
        public static BuddyAddEvent onAdd = new BuddyAddEvent(); // add

        public uint Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public uint Icon { get; set; }
        public bool NotifyLogin { get; set; }
        public BuddyStatus Status { get; set; }
        public List<byte> Groups {get; set; } // TODO: actually support these

        public void SetStatus(BuddyStatus status) {
            Status = status;
            onStatusChange.Invoke(this, status);
        }

        public void SetIcon(uint icon) {
            Icon = icon;
            onIconChange.Invoke(this, icon);
        }

        public void SetDesc(string desc) {
            Desc = desc;
            onDescChange.Invoke(this, desc);
        }
    }
}
