using System;
using Bug.MiniGame;
using SaveData;
using UnityEngine;

namespace PlayerController
{
    public class GeneralInputReader : MonoBehaviour
    {
        public static Action ClickAction { get; set; }
        public static Action PdgClickAction { get; set; }
        public static Action EnterAction { get; set; }
        
        public static bool MenuValue { get; set; }
        public static Action MenuAction { get; set; }
        public static Action ExitAction { get; set; }
        
        public static Action Num0Action { get; set; }
        public static Action Num1Action { get; set; }
        public static Action Num2Action { get; set; }
        public static Action Num3Action { get; set; }
        public static Action Num4Action { get; set; }
        public static Action Num5Action { get; set; }
        public static Action Num6Action { get; set; }
        public static Action Num7Action { get; set; }
        public static Action Num8Action { get; set; }
        public static Action Num9Action { get; set; }
        public static Action ReturnAction { get; set; }

        public void OnClickAction()
        {
            ClickAction?.Invoke();
        }
        public static void OnStaticClickAction() => ClickAction?.Invoke();
        
        public void OnPdgClickAction() => PdgClickAction?.Invoke();
        public static void OnStaticPdgClickAction() => PdgClickAction?.Invoke();
        
        public void OnEnterAction() => EnterAction?.Invoke();
        
        public void OnMenu()
        {
            if (MiniGameManager.IsOnMiniGame)
            {
                Exit();
                return;
            }

            Menu();
        }
        public static void OnSaticMenu()
        {
            if (MiniGameManager.IsOnMiniGame)
            {
                Exit();
                return;
            }

            Menu();
        }

        private static void Exit()
        {
            ExitAction?.Invoke();
        }

        private static void Menu()
        {
            MenuValue = !MenuValue;
            MenuAction?.Invoke();
            
            if (!MenuValue)
                SaveLoadData.Save();
        }
        
        public void OnNum0() => Num0Action?.Invoke();
        public void OnNum1() => Num1Action?.Invoke();
        public void OnNum2() => Num2Action?.Invoke();
        public void OnNum3() => Num3Action?.Invoke();
        public void OnNum4() => Num4Action?.Invoke();
        public void OnNum5() => Num5Action?.Invoke();
        public void OnNum6() => Num6Action?.Invoke();
        public void OnNum7() => Num7Action?.Invoke();
        public void OnNum8() => Num8Action?.Invoke();
        public void OnNum9() => Num9Action?.Invoke();
        public void OnReturn() => ReturnAction?.Invoke();
        
        #region Dev
        
        public static Action DevEarnMoneyAction { get; set; }
        private void OnDevEarnMoney() => DevEarnMoneyAction?.Invoke();

        #endregion
    }
}
