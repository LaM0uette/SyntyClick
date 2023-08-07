using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Bug.MiniGame
{
    public class RubiksColorHandler : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;
        [SerializeField] private Button[] _buttons;
        
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
                button.image.color = _colors[Random.Range(0, _colors.Length)];
            }
        }

        public void SetButtonColor(Button button)
        {
            var currentColor = button.image.color;

            while (button.image.color == currentColor)
            {
                button.image.color = _colors[Random.Range(0, _colors.Length)];
            }

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
