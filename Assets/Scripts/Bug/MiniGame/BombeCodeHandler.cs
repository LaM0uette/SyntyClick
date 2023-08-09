using System.Collections;
using Audio;
using PlayerController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bug.MiniGame
{
    public class BombeCodeHandler : MonoBehaviour
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
            GeneralInputReader.EnterAction += CodeValidation;
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
            GeneralInputReader.ReturnAction += DeleteLastNumber;
        }

        private void OnDisable()
        {
            GeneralInputReader.EnterAction -= CodeValidation;
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
            GeneralInputReader.ReturnAction -= DeleteLastNumber;
        }

        public void OnNumButtonClick(int num)
        {
            MusicManager.instance.MmfBeep.PlayFeedbacks();
            
            if (_tmpResult.text.Length >= 4) return;
            _tmpResult.text += num;
        }

        #endregion
        
        #region Functions

        private void SetInitialCalcul()
        {
            var randomCode = Random.Range(1000, 9999);
            
            _tmpCalcul.text = randomCode.ToString();
            _result = randomCode;
        }

        public void CodeValidation()
        {
            MusicManager.instance.MmfBeep.PlayFeedbacks();

            if (_tmpResult.text.Length <= 0)
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
        
        public void DeleteLastNumber()
        {
            MusicManager.instance.MmfBeep.PlayFeedbacks();
            
            if (_tmpResult.text.Length <= 0) return;
            _tmpResult.text = _tmpResult.text.Remove(_tmpResult.text.Length - 1);
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
            
            MiniGameManager.BugValid?.Invoke();
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
