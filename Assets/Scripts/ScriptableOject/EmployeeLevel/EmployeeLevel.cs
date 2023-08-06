using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableOject.EmployeeLevel
{
    [CreateAssetMenu(fileName = "New EmployeeLevel", menuName = "EmployeeLevel")]
    public class EmployeeLevel : ScriptableObject
    {
        public int Level;
        public int CostLevel;
        public Material Material;
        public float IncrementAmount;
        public float IncrementClickAmount;
        public int MaxAssets;
        public int FansGainAmout;
        public int MoneyGainAmout;
        [FormerlySerializedAs("ChanceGetBug")] [Range(0, 1)] public float ChanceBug;
    }
}
