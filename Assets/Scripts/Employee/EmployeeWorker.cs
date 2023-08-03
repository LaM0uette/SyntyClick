using System.Collections;
using PlayerController;
using ScriptableOject.EmployeeLevel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Employee
{
    public class EmployeeWorker : MonoBehaviour
    {
        #region Statements
        
        private static GameManager _gameManager => GameManager.instance;
        private static ObjectiveManager _objectiveManager => ObjectiveManager.instance;
        private InputReader _playerInputs;
        
        [Header("Animator")]
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Pause = Animator.StringToHash("Pause");
        private static readonly int Stop = Animator.StringToHash("Stop");
        [SerializeField] private Animator _employeeAnimator;
        
        [Header("EmployeeLevel")]
        [SerializeField] private EmployeeLevel[] _employeeLevels;
        private EmployeeLevel _currentEmployeeLevel;
        
        [Header("RadialSprite")]
        [SerializeField] private Image _spriteProgress;
        [SerializeField] private Image _spriteProgressStop;
        [SerializeField] private TextMeshProUGUI _TmpMaxAssets;
        
        private Camera _mainCamera;
        private float _pieceInProgress;
        private int _currentAssetsOnWorked;
        private bool _isPaused;
        private EmployeeWorker _employeeWorker;

        private void Awake()
        {
            _playerInputs = GetComponent<InputReader>();
            _employeeWorker = GetComponent<EmployeeWorker>();
            _mainCamera = Camera.main;
            _currentEmployeeLevel = _employeeLevels[0];
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            _playerInputs.ClickAction += OnClickAction;
            _playerInputs.OnClickGameObject += OnMouseLeftClickAction;
            _playerInputs.MouseRightClickAction += OnMouseRightClickAction;
        }
        
        private void OnDisable()
        {
            _playerInputs.ClickAction -= OnClickAction;
            _playerInputs.OnClickGameObject -= OnMouseLeftClickAction;
            _playerInputs.MouseRightClickAction -= OnMouseRightClickAction;
        }

        private void Update()
        {
            if (CheckMaxAssets()) return;
            
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
        
        private void OnMouseLeftClickAction(GameObject clickedObject)
        {
            if (clickedObject == _employeeWorker.gameObject)
            {
                Debug.Log("Reset _currentAssetsOnWorked + add Fans ans Money");
                _gameManager.IncrementAssets(_currentAssetsOnWorked);
                _employeeAnimator.SetTrigger(Stop);
                _gameManager.Fans += 1;
                _gameManager.Money += 50;
                    
                _spriteProgressStop.fillAmount = 0;
                _currentAssetsOnWorked = 0;
                _TmpMaxAssets.text = "0";
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

        private bool CheckMaxAssets()
        {
            if (_currentAssetsOnWorked >= _currentEmployeeLevel.MaxAssets)
            {
                _spriteProgressStop.fillAmount = 1;
                _TmpMaxAssets.text = "MAX";
            
                if (!_isPaused)
                {
                    _employeeAnimator.SetTrigger(Pause);
                    _isPaused = true;
                }

                return true;
            }

            if (_isPaused)
            {
                _isPaused = false;
            }

            return false;
        }
        
        private void PieceIncrement(float incrementAmount)
        {
            _pieceInProgress += incrementAmount;
            _spriteProgress.fillAmount = _pieceInProgress / _objectiveManager.CurrentObjectives.IncrementDelay;
        }
        
        private void TryIncrementAssets()
        {
            if(_pieceInProgress >= _objectiveManager.CurrentObjectives.IncrementDelay)
            {
                _pieceInProgress = 0;
                
                _currentAssetsOnWorked++;
                _TmpMaxAssets.text = $"{_currentAssetsOnWorked}";
                
                _spriteProgress.fillAmount = 0;
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
