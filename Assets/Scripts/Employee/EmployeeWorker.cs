using System;
using System.Collections;
using JetBrains.Annotations;
using PlayerController;
using ScriptableOject.EmployeeLevel;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("_TmpMaxAssets")] [SerializeField] private TextMeshProUGUI _tmpMaxAssets;
        
        private float _pieceInProgress;
        private int _currentAssetsOnWorked;
        private bool _isPaused;
        private EmployeeWorker _employeeWorker;
        [CanBeNull] private EmployeeWorker _employeeWorkerClicked;

        private void Awake()
        {
            _playerInputs = GetComponent<InputReader>();
            _employeeWorker = GetComponent<EmployeeWorker>();
            _currentEmployeeLevel = _employeeLevels[0];
        }

        private void Start()
        {
            StartCoroutine(IncrementFansAndMoneyAllTime());
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            _playerInputs.ClickAction += OnClickAction;
            _playerInputs.ClickGameObject += OnClickGameObject;
            _playerInputs.MouseLeftClickAction += OnMouseLeftClickAction;
            _playerInputs.MouseRightClickAction += OnMouseRightClickAction;
        }
        
        private void OnDisable()
        {
            _playerInputs.ClickAction -= OnClickAction;
            _playerInputs.ClickGameObject -= OnClickGameObject;
            _playerInputs.MouseLeftClickAction -= OnMouseLeftClickAction;
            _playerInputs.MouseRightClickAction -= OnMouseRightClickAction;
        }

        private void Update()
        {
            if (_isPaused) return;
            
            PieceIncrement(_currentEmployeeLevel.IncrementAmount * Time.deltaTime);
            TryIncrementAssets();
        }

        private void FixedUpdate()
        {
            CheckMaxAssets();
        }

        #endregion

        #region Functions
        
        private IEnumerator IncrementFansAndMoneyAllTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(10f);
                
                var amountFans = (int)((float)_gameManager.TotalAssets / 100 * _currentEmployeeLevel.FansGainAmout);
                var amountMoney = amountFans * _currentEmployeeLevel.MoneyGainAmout;
                
                IncrementFansAndMoney(amountFans, amountMoney);
            }
            
        }
        
        private void CheckMaxAssets()
        {
            if (!_isPaused && !_employeeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                _employeeAnimator.SetTrigger(Stop);
            }
            
            if (_currentAssetsOnWorked >= _currentEmployeeLevel.MaxAssets)
            {
                if (_isPaused && !_employeeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pause"))
                {
                    _employeeAnimator.SetTrigger(Pause);
                }
                
                if (!_isPaused)
                {
                    _isPaused = true;
                    _employeeAnimator.SetTrigger(Pause);
                    
                    _spriteProgressStop.fillAmount = 1;
                    _tmpMaxAssets.text = "MAX";
                }
                
                return;
            }
            
            if (_isPaused)
            {
                _isPaused = false;
                _employeeAnimator.SetTrigger(Stop);
            }
        }

        private void OnClickAction()
        {
            AnimatorSetSpeed(GameManager.SpeedBoost);
            PieceIncrement(_currentEmployeeLevel.IncrementClickAmount);
            StartCoroutine(ResetSpeed());
        }
        
        private void AnimatorSetSpeed(float speed)
        {
            _employeeAnimator.SetFloat(Speed, speed);
        }

        private void OnClickGameObject(GameObject clickedObject)
        {
            _employeeWorkerClicked = clickedObject.TryGetComponent<EmployeeWorker>(out var employeeWorker)
                ? employeeWorker
                : null;
        }

        private void OnMouseLeftClickAction()
        {
            if (_employeeWorkerClicked != _employeeWorker) return;
            AddAssetsOnWorked();
        }
        
        private void AddAssetsOnWorked()
        {
            _gameManager.IncrementAssets(_currentAssetsOnWorked);
            
            IncrementFansAndMoney();
            ResetAll();
        }

        private void IncrementFansAndMoney()
        {
            var amountFans = (int)((_currentAssetsOnWorked + (float)_gameManager.TotalAssets / 10) / 100 * _currentEmployeeLevel.FansGainAmout);
            var amountMoney = amountFans * _currentEmployeeLevel.MoneyGainAmout;
            
            IncrementFansAndMoney(amountFans, amountMoney);
        }
        private static void IncrementFansAndMoney(int amountFans, int amountMoney)
        {
            _gameManager.IncrementFans(amountFans);
            _gameManager.IncrementMoney(amountMoney);
        }
        
        private void OnMouseRightClickAction()
        {
            if (_employeeWorkerClicked != _employeeWorker) return;
            
            LevelUp();
        }
        
        private void LevelUp()
        {
            if (_currentEmployeeLevel.Level >= _employeeLevels.Length)
            {
                return;
            }
    
            _currentEmployeeLevel = _employeeLevels[_currentEmployeeLevel.Level];
            AddAssetsOnWorked();
        }
        
        private void PieceIncrement(float incrementAmount)
        {
            _pieceInProgress += incrementAmount;
            SetSpriteProgress();
        }
        
        private void SetSpriteProgress()
        {
            _spriteProgress.fillAmount = _pieceInProgress / _objectiveManager.CurrentObjectives.IncrementDelay;
        }
        
        private void TryIncrementAssets()
        {
            if(_pieceInProgress >= _objectiveManager.CurrentObjectives.IncrementDelay)
            {
                IncrementCurrentAssetsOnWorked();
                ResetPieceInProgress();
                ResetSpriteProgress();
            }
        }

        private void IncrementCurrentAssetsOnWorked()
        {
            _tmpMaxAssets.text = $"{++_currentAssetsOnWorked}";
        }

        private void ResetAll()
        {
            
            ResetPieceInProgress();
            ResetSpriteProgress();
            ResetSpriteProgressStop();
            ResetCurrentAssetsOnWorked();
            ResetTmpMaxAssets();
        }
        private void ResetPieceInProgress() => _pieceInProgress = 0;
        private void ResetSpriteProgress() => _spriteProgress.fillAmount = 0;
        private void ResetSpriteProgressStop() => _spriteProgressStop.fillAmount = 0;
        private void ResetCurrentAssetsOnWorked() => _currentAssetsOnWorked = 0;
        private void ResetTmpMaxAssets() => _tmpMaxAssets.text = "0";
        
        private IEnumerator ResetSpeed()
        {
            yield return new WaitForSeconds(.1f);
            AnimatorSetSpeed(GameManager.SpeedNormal);
        }

        

        #endregion
    }
}
