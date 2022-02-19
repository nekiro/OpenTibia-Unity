using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OpenTibiaUnity.Core.BuddyList
{
    public class BuddyStorage
    {
        private Dictionary<uint, Buddy> _buddies = new Dictionary<uint, Buddy>();

        public void AddBuddy(uint creatureId, Buddy buddy) {
            _buddies[creatureId] = buddy;
            Buddy.onAdd.Invoke(buddy);
        }

        public void RemoveBuddy(Buddy buddy) {
            Buddy.onRemove.Invoke(buddy);
            _buddies.Remove(buddy.Id);
        }

        public Buddy GetBuddy(uint creatureId) {
            if (!_buddies.ContainsKey(creatureId)) {
                return null;
            }
            return _buddies[creatureId];
        }

        public Buddy GetBuddy(string name) {
            return _buddies.SingleOrDefault(b => b.Value.Name.Equals(name)).Value;
        }

        public void SetBuddyState(uint creatureId, BuddyStatus status) {
            Buddy buddy = GetBuddy(creatureId);
            if (buddy != null) {
                buddy.SetStatus(status);
            }
        }

        public void SetBuddyIcon(uint creatureId, uint icon) {
            Buddy buddy = GetBuddy(creatureId);
            if (buddy != null) {
                buddy.SetIcon(icon);
            }
        }

        public void SetBuddyDesc(uint creatureId, string desc) {
            Buddy buddy = GetBuddy(creatureId);
            if (buddy != null) {
                buddy.SetDesc(desc);
            }
        }

        public Dictionary<uint, Buddy> GetBuddies() {
            return _buddies;
        }
    }
}