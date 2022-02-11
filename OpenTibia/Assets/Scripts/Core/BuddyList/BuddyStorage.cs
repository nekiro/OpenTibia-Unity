using System.Collections.Generic;
using System.Linq;

namespace OpenTibiaUnity.Core.BuddyList
{
    public class BuddyStorage
    {
        private Dictionary<uint, Buddy> _buddies = new Dictionary<uint, Buddy>();

        public void AddBuddy(uint creatureId, Buddy buddy) {
            _buddies.Add(creatureId, buddy);
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

        public Dictionary<uint, Buddy> GetBuddies() {
            return _buddies;
        }
    }
}