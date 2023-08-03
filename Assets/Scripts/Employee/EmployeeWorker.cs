using System.Collections;
using PlayerController;
using ScriptableOject.EmployeeLevel;
using UnityEngine;

namespace Employee
{
    public class EmployeeWorker : MonoBehaviour
    {
        #region Statements
        
        private static GameManager _gameManager => GameManager.instance;
        private static ObjectiveManager _objectiveManager => ObjectiveManager.instance;
        private static InputReader _playerInputs;
        
        [Header("Animator")]
        private static readonly int Speed = Animator.StringToHash("Speed");
        [SerializeField] private Animator _employeeAnimator;
        
        [Header("EmployeeLevel")]
        [SerializeField] private EmployeeLevel[] _employeeLevels;
        private EmployeeLevel _currentEmployeeLevel;
        
        private Camera _mainCamera;
        private float _pieceInProgress;

        private void Awake()
        {
            _playerInputs = GetComponent<InputReader>();
            _mainCamera = Camera.main;
            _currentEmployeeLevel = _employeeLevels[0];
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            _playerInputs.ClickAction += OnClickAction;
            _playerInputs.MouseRightClickAction += OnMouseRightClickAction;
        }
        
        private void OnDisable()
        {
            _playerInputs.ClickAction -= OnClickAction;
            _playerInputs.MouseRightClickAction -= OnMouseRightClickAction;
        }

        private void Update()
        {
            PieceIncrement(_currentEmployeeLevel.IncrementAmount * Time.deltaTime);
            TryIncrementAssets();
        }

        #endregion

        #region Functions

        private void OnClickAction()
        {
            AnimatorSetSpeed(GameManager.SpeedBoost);
            PieceIncrement(_currentEmployeeLevel.IncrementClickAmount);
            StartCoroutine(ResetSpeed());
        }
        
        private void OnMouseLeftClickAction()
        {
            var ray = _mainCamera.ScreenPointToRay(_playerInputs.MousePositionValue);

            if (Physics.Raycast(ray, out var hit))
            {
                var employee = hit.transform.GetComponent<EmployeeWorker>();
                if (employee is not null)
                {
                    Debug.Log("Employee");
                }
            }
        }
        
        private void OnMouseRightClickAction()
        {
            var ray = _mainCamera.ScreenPointToRay(_playerInputs.MousePositionValue);

            if (Physics.Raycast(ray, out var hit))
            {
                var employee = hit.transform.GetComponent<EmployeeWorker>();
                if (employee is not null)
                {
                    LevelUp();
                }
            }
        }
        
        private void AnimatorSetSpeed(float speed)
        {
            _employeeAnimator.SetFloat(Speed, speed);
        }

        private void PieceIncrement(float incrementAmount)
        {
            _pieceInProgress += incrementAmount;
        }
        
        private void TryIncrementAssets()
        {
            if(_pieceInProgress >= _objectiveManager.CurrentObjectives.IncrementDelay)
            {
                _pieceInProgress = 0;
                _gameManager.IncrementAssets();
            }
        }
        
        private IEnumerator ResetSpeed()
        {
            yield return new WaitForSeconds(.1f);
            AnimatorSetSpeed(GameManager.SpeedNormal);
        }

        private void LevelUp()
        {
            if (_currentEmployeeLevel.Level >= _employeeLevels.Length)
            {
                Debug.Log("Max level reached.");
                return;
            }
    
            _currentEmployeeLevel = _employeeLevels[_currentEmployeeLevel.Level];
            Debug.Log($"Level up to {_currentEmployeeLevel.Level}");
        }

        #endregion
    }
}
