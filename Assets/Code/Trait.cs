using UnityEngine;

namespace Gameplay {
    [System.Serializable]
    public class Trait {
        public TraitData data;

        public Trait(TraitData data) {
            this.data = data;
        }
    }

    [System.Serializable]
    public class TraitData {
        public string title;
        public Color debugColor = Color.white;
        public string hates;
    }
}
