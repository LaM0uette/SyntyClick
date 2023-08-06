using System;
using UnityEngine;

namespace PlayerController
{
    public class GeneralInputReader : MonoBehaviour
    {
        public static Action ClickAction { get; set; }
        public static Action PdgClickAction { get; set; }
        
        public static bool MenuValue { get; set; }
        public static Action MenuAction { get; set; }
        
        public static void OnClickAction() => ClickAction?.Invoke();
        public static void OnPdgClickAction() => PdgClickAction?.Invoke();
        
        public void OnMenu()
        {
            MenuValue = !MenuValue;
            MenuAction?.Invoke();
        }
        
        public static void OnStaticMenu()
        {
            MenuValue = !MenuValue;
            MenuAction?.Invoke();
        }
        
        #region Dev
        
        public static Action DevEarnMoneyAction { get; set; }
        private void OnDevEarnMoney() => DevEarnMoneyAction?.Invoke();

        #endregion
    }
}
