using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bug.MiniGame
{
    public class NumberSuiteHandler : MonoBehaviour
    {
        #region Statements

        [SerializeField] private Color _defaultButtonColor;
        [SerializeField] private Color _selectedButtonColor;
        [SerializeField] private Button[] _buttons;
        
        private readonly Dictionary<Button, int> _buttonIndices = new();
        private int _currentButtonCount;

        #endregion
        
        #region Events

        private void OnEnable()
        {
            SetInitialNumbers();
        }

        public void OnNumButtonClick(Button button)
        {
            var buttonId = _buttonIndices[button];
            
            if (buttonId == 10 && _currentButtonCount == 9)
            {
                button.image.color = _selectedButtonColor;
                
                FinishValid();
                return;
            }
            
            if (buttonId == _currentButtonCount + 1)
            {
                button.image.color = _selectedButtonColor;
                
                _currentButtonCount++;
                return;
            }
            
            button.image.color = Color.red;
            FinishError();
        }

        #endregion

        #region Functions
        
        private void SetInitialNumbers()
        {
            foreach (var button in _buttons)
            {
                button.interactable = true;
            }
            
            var numbers = Enumerable.Range(1, 10).ToList();

            var rnd = new System.Random();
            numbers = numbers.OrderBy(x => rnd.Next()).ToList();

            var i = 0;
            foreach (var button in _buttons)
            {
                if(i >= numbers.Count)
                    break;
                
                button.image.color = _defaultButtonColor;
                _buttonIndices[button] = numbers[i];
                
                var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if(buttonText != null)
                    buttonText.text = numbers[i].ToString();

                i++;
            }

            foreach (var buttonIndice in _buttonIndices)
            {
                Debug.Log($"{buttonIndice.Key} : {buttonIndice.Value}");
            }
        }

        private void FinishError()
        {
            foreach (var button in _buttons)
            {
                button.interactable = false;
            }
            
            Finish();
        }
        
        private void FinishValid()
        {
            MiniGameManager.AddFansAndMoney();
            
            Finish();
        }
        
        private void Finish()
        {
            StartCoroutine(Exit());
        }
        
        private IEnumerator Exit()
        {
            yield return new WaitForSeconds(1.4f);
            
            transform.gameObject.SetActive(false);
            MiniGameManager.BugCorrectedAction?.Invoke(MiniGameManager.CurrentEmployeeWorker);
        }

        #endregion
    }
}
