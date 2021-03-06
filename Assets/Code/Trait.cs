using UnityEngine;

namespace Gameplay {
    [CreateAssetMenu(fileName = "New Trait", menuName = "Trait Data")]
    public class Trait : ScriptableObject
    {
        [SerializeField]
        public string TraitName;
        [SerializeField]
        public int TraitID;
        [SerializeField]
        public Texture2D Sprites;
        [SerializeField]
        public Texture2D Icon;
        [SerializeField]
        public Texture2D SpriteIcon;
        [SerializeField]
        public Trait Hate;
        [SerializeField]
        public Color TraitColor;

        public bool Conflicts(Trait other)
        {
            if (other == this)
            {
                return false;
            }
            if (Hate == other || other.Hate == this)
            {
                return true;
            }
            return false;
        }

        //Music & other assests
    }
}
