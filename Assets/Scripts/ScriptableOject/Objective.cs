using UnityEngine;

namespace ScriptableOject
{
    [CreateAssetMenu(fileName = "New Objective", menuName = "Objective")]
    public class Objective : ScriptableObject
    {
        public string Name;
        public Sprite Image;
        public int AssetCount;
        public bool isInfinite;
    }
}
