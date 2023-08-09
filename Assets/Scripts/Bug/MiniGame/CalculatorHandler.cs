using System.Collections;
using Audio;
using PlayerController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bug.MiniGame
{
    public class CalculatorHandler : MonoBehaviour
    {
        #region Statements

        [SerializeField] private TextMeshProUGUI _tmpCalcul;
        [SerializeField] private TextMeshProUGUI _tmpResult;
        
        [SerializeField] private Image _screen;
        [SerializeField] private Color _screenColor;

        private int _result;

        #endregion

        #region Events

        private void OnEnable()
        {
            GeneralInputReader.EnterAction += CalculValidation;
            GeneralInputReader.ExitAction += FinishError;
            
            _screen.color = _screenColor;
            _tmpResult.text = string.Empty;
            
            SetInitialCalcul();
            
            GeneralInputReader.Num0Action += () => OnNumButtonClick(0);
            GeneralInputReader.Num1Action += () => OnNumButtonClick(1);
            GeneralInputReader.Num2Action += () => OnNumButtonClick(2);
            GeneralInputReader.Num3Action += () => OnNumButtonClick(3);
            GeneralInputReader.Num4Action += () => OnNumButtonClick(4);
            GeneralInputReader.Num5Action += () => OnNumButtonClick(5);
            GeneralInputReader.Num6Action += () => OnNumButtonClick(6);
            GeneralInputReader.Num7Action += () => OnNumButtonClick(7);
            GeneralInputReader.Num8Action += () => OnNumButtonClick(8);
            GeneralInputReader.Num9Action += () => OnNumButtonClick(9);
        }

        private void OnDisable()
        {
            GeneralInputReader.EnterAction -= CalculValidation;
            GeneralInputReader.ExitAction -= FinishError;
            
            GeneralInputReader.Num0Action -= () => OnNumButtonClick(0);
            GeneralInputReader.Num1Action -= () => OnNumButtonClick(1);
            GeneralInputReader.Num2Action -= () => OnNumButtonClick(2);
            GeneralInputReader.Num3Action -= () => OnNumButtonClick(3);
            GeneralInputReader.Num4Action -= () => OnNumButtonClick(4);
            GeneralInputReader.Num5Action -= () => OnNumButtonClick(5);
            GeneralInputReader.Num6Action -= () => OnNumButtonClick(6);
            GeneralInputReader.Num7Action -= () => OnNumButtonClick(7);
            GeneralInputReader.Num8Action -= () => OnNumButtonClick(8);
            GeneralInputReader.Num9Action -= () => OnNumButtonClick(9);
        }

        public void OnNumButtonClick(int num)
        {
            MusicManager.instance.MmfClick.PlayFeedbacks();
            
            if (_tmpResult.text.Length >= 4) return;
            _tmpResult.text += num;
        }

        #endregion

        #region Functions

        private void SetInitialCalcul()
        {
            var num1 = Random.Range(1, 999);
            var num2 = Random.Range(1, 999);
            
            _result = num1 + num2;
            _tmpCalcul.text = $"{num1} + {num2} = ??";
        }

        public void CalculValidation()
        {
            MusicManager.instance.MmfClick.PlayFeedbacks();
            
            if (_tmpResult.text.Length is > 9999 or <= 0)
            {
                FinishError();
                return;
            }
            
            if (int.Parse(_tmpResult.text) == _result)
            {
                FinishValid();
            }
            else
            {
                FinishError();
            }
        }
        
        private void FinishError()
        {
            if (MiniGameManager.IsOnHint) return;
            
            _screen.color = Color.red;
            
            MiniGameManager.BugError?.Invoke();
            MusicManager.instance.MmfError.PlayFeedbacks();
            Finish();
        }
        
        private void FinishValid()
        {
            MiniGameManager.AddFansAndMoney();
            
            _screen.color = Color.green;
            
            MusicManager.instance.MmfValidation.PlayFeedbacks();
            Finish();
        }
        
        private void Finish()
        {
            StartCoroutine(ExitHashWord());
        }
        
        private IEnumerator ExitHashWord()
        {
            yield return new WaitForSeconds(1.4f);
            
            transform.gameObject.SetActive(false);
            MiniGameManager.ResetIsOnMiniGame();
            MiniGameManager.BugCorrectedAction?.Invoke(MiniGameManager.CurrentEmployeeWorker);
        }

        #endregion
    }
}