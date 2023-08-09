using System.Collections;
using System.Collections.Generic;
using Audio;
using PlayerController;
using UnityEngine;
using UnityEngine.UI;

namespace Bug.MiniGame
{
    public class RubiksColorHandler : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;
        [SerializeField] private Button[] _buttons;
        
        private readonly Dictionary<Button, int> _buttonColorIndices = new();
        
        #region Events

        private void OnEnable()
        {
            SetInitialColors();
            GeneralInputReader.ExitAction += FinishError;
        }

        private void OnDisable()
        {
            GeneralInputReader.ExitAction -= FinishError;
        }

        #endregion

        #region Functions

        private void SetInitialColors()
        {
            foreach (var button in _buttons)
            {
                button.interactable = true;
                _buttonColorIndices[button] = Random.Range(0, _colors.Length);
                button.image.color = _colors[_buttonColorIndices[button]];
            }
        }

        public void SetButtonColor(Button button)
        {
            MusicManager.instance.MmfClick.PlayFeedbacks();
            
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
        
        private void FinishError()
        {
            if (MiniGameManager.IsOnHint) return;
            
            foreach (var button in _buttons)
            {
                button.interactable = false;
                button.image.color = Color.red;
            }
            
            MiniGameManager.BugError?.Invoke();
            MusicManager.instance.MmfError.PlayFeedbacks();
            
            StartCoroutine(FinishWin());
        }
        
        private void Finish()
        {
            foreach (var button in _buttons)
            {
                button.interactable = false;
                button.image.color = Color.green;
            }
            
            MusicManager.instance.MmfValidation.PlayFeedbacks();
            MiniGameManager.AddFansAndMoney();
            
            StartCoroutine(FinishWin());
        }
        
        private IEnumerator FinishWin()
        {
            yield return new WaitForSeconds(0.8f);
            
            transform.gameObject.SetActive(false);
            MiniGameManager.ResetIsOnMiniGame();
            MiniGameManager.BugCorrectedAction?.Invoke(MiniGameManager.CurrentEmployeeWorker);
        }

        #endregion
    }
}
