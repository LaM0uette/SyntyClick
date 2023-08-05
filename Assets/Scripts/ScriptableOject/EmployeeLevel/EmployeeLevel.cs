using UnityEngine;

namespace ScriptableOject.EmployeeLevel
{
    [CreateAssetMenu(fileName = "New EmployeeLevel", menuName = "EmployeeLevel")]
    public class EmployeeLevel : ScriptableObject
    {
        public int Level;
        public Material Material;
        public float IncrementAmount;
        public float IncrementClickAmount;
        public int MaxAssets;
        public int FansGainAmout;
        public int MoneyGainAmout;
    }
}
