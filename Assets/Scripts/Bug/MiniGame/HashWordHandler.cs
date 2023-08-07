using System;
using Employee;
using PlayerController;
using TMPro;
using UnityEngine;
namespace Bug.MiniGame
{
    public class HashWordHandler : MonoBehaviour
    {
        #region Functions
        
        [SerializeField] private TMP_InputField _inputDecodeWord;
        
        private string _decodeWord;

        private void OnEnable()
        {
            MiniGameManager.ClickedEmployeeWorker = InputReader.ClickedGameObject.TryGetComponent<EmployeeWorker>(out var employeeWorker)
                ? employeeWorker
                : null;
        }

        public void OnLineEditChange()
        {
            _decodeWord = _inputDecodeWord.text;
            Debug.Log(_decodeWord);
        }

        public void Decode()
        {
            transform.gameObject.SetActive(false);
        }

        #endregion
    }
}
