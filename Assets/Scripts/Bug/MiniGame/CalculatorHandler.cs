﻿using System.Collections;
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
            
            _screen.color = _screenColor;
            _tmpResult.text = string.Empty;
            
            SetInitialCalcul();
        }

        private void OnDisable()
        {
            GeneralInputReader.EnterAction -= CalculValidation;
        }

        public void OnNumButtonClick(int num)
        {
            MusicManager.instance.MmfClick.PlayFeedbacks();
            _tmpResult.text += num;
        }

        #endregion

        #region Functions

        private void SetInitialCalcul()
        {
            var num1 = UnityEngine.Random.Range(1, 999);
            var num2 = UnityEngine.Random.Range(1, 999);
            
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