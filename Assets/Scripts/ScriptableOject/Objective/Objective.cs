using UnityEngine;

namespace ScriptableOject.Objective
{
    [CreateAssetMenu(fileName = "New Objective", menuName = "Objective")]
    public class Objective : ScriptableObject
    {
        public int Id;
        public string Name;
        public Sprite Image;
        public int AssetCount;
        public int IncrementDelay;
        public bool isInfinite;
    }
}
