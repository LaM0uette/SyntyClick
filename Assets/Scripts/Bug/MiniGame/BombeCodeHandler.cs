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
            
            _screen.color = _screenColor;
            _tmpResult.text = string.Empty;
            
            SetInitialCalcul();
        }

        private void OnDisable()
        {
            GeneralInputReader.EnterAction -= CodeValidation;
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
            var randomCode = Random.Range(1000, 9999);
            
            _tmpCalcul.text = randomCode.ToString();
            _result = randomCode;
        }

        public void CodeValidation()
        {
            MusicManager.instance.MmfClick.PlayFeedbacks();
            
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
            MusicManager.instance.MmfClick.PlayFeedbacks();
            
            if (_tmpResult.text.Length <= 0) return;
            _tmpResult.text = _tmpResult.text.Remove(_tmpResult.text.Length - 1);
        }
        
        private void FinishError()
        {
            _screen.color = Color.red;
            
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
            MiniGameManager.BugCorrectedAction?.Invoke(MiniGameManager.CurrentEmployeeWorker);
        }

        #endregion
    }
}
