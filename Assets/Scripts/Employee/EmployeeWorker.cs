using PlayerController;
using UnityEngine;

namespace Employee
{
    public class EmployeeWorker : MonoBehaviour
    {
        #region Statements

        private static GameManager _gameManager => GameManager.instance;
        private static InputReader _playerInputs;
        
        private const int _incrementDelay  = 1;
        private const float _incrementAmount = 0.1f;
        private const float _incrementClickAmount = 0.2f;
        
        public float _pieceInProgress;

        private void Awake()
        {
            _playerInputs = GetComponent<InputReader>();
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            _playerInputs.ClickAction += () => PieceIncrement(_incrementClickAmount);
        }

        private void Update()
        {
            PieceIncrement(_incrementAmount * Time.deltaTime);
            
            if (_pieceInProgress >= _incrementDelay)
            {
                _pieceInProgress = 0;
                _gameManager.IncrementAssets();
            }
        }

        #endregion

        #region Functions

        private void PieceIncrement(float incrementAmount)
        {
            _pieceInProgress += incrementAmount;
        }

        #endregion
    }
}
