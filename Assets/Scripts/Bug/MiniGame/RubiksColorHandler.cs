using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bug.MiniGame
{
    public class RubiksColorHandler : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;
        [SerializeField] private Button[] _buttons;
        
        private Dictionary<Button, int> _buttonColorIndices = new Dictionary<Button, int>();
        
        #region Events

        private void OnEnable()
        {
            SetInitialColors();
        }

        #endregion

        #region Functions

        private void SetInitialColors()
        {
            foreach (var button in _buttons)
            {
                _buttonColorIndices[button] = Random.Range(0, _colors.Length);
                button.image.color = _colors[_buttonColorIndices[button]];
            }
        }

        public void SetButtonColor(Button button)
        {
            _buttonColorIndices[button] = (_buttonColorIndices[button] + 1) % _colors.Length;
            button.image.color = _colors[_buttonColorIndices[button]];
            CheckWin();
        }

        private void CheckWin()
        {
            var currentColor = _buttons[0].image.color;
            
            foreach (var button in _buttons)
            {
                if (currentColor != button.image.color) return;
            }
            
            Finish();
        }
        
        private void Finish()
        {
            foreach (var button in _buttons)
            {
                button.interactable = false;
                button.image.color = Color.green;
            }
            
            StartCoroutine(FinishWin());
        }
        
        private IEnumerator FinishWin()
        {
            yield return new WaitForSeconds(0.8f);
            
            MiniGameManager.AddFansAndMoney();
            transform.gameObject.SetActive(false);
            MiniGameManager.BugCorrectedAction?.Invoke(MiniGameManager.CurrentEmployeeWorker);
        }

        #endregion
    }
}