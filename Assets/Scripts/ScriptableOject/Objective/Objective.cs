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
        public bool isInfinite;
        public int IncrementDelay;
        public int FansGainAmout;
        public int MoneyGainAmout;
        public IconProps.IconProps IconProps;
    }
}
