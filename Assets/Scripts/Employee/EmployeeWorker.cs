using System;
using System.Collections;
using EPOOutline;
using JetBrains.Annotations;
using PlayerController;
using SaveData;
using ScriptableOject.EmployeeLevel;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

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
        [SerializeField, Range(0.6f, 1.4f)] private float _speedNormal = 1f;
        
        [Header("EmployeeLevel")]
        [SerializeField] private EmployeeLevel[] _employeeLevels;
        private EmployeeLevel _currentEmployeeLevel;
        
        [Header("EmployeeAssets")]
        [SerializeField] private Renderer _desktopRenderer;
        [SerializeField] private string _employeeName;
        
        [Header("RadialSprite")]
        [SerializeField] private Image _spriteProgress;
        [SerializeField] private Image _spriteProgressStop;
        [SerializeField] private TextMeshProUGUI _tmpMaxAssets;
        [SerializeField] private Image _spriteAssetOnWorked;
        [SerializeField] private TextMeshProUGUI _tmpCostLvlUp;
        [SerializeField] private GameObject _prefabButtonLvlUp;
        [SerializeField] private LevelButtonManager _levelButtonManager;
        
        private float _pieceInProgress;
        private int _currentAssetsOnWorked;
        private bool _isPaused;
        private EmployeeWorker _employeeWorker;
        [CanBeNull] private EmployeeWorker _employeeWorkerClicked;
        
        [SerializeField] private int _id;
        [SerializeField] private GameObject _prefabBug;
        [SerializeField] private GameObject _prefabBugButtonLvl;
        [SerializeField] private Outlinable _outlinable;
        [SerializeField] private Outlinable _outlinableBug;
        private bool _isBug;

        private void Awake()
        {
            _playerInputs = GetComponent<InputReader>();
            _employeeWorker = GetComponent<EmployeeWorker>();
        }

        private void Start()
        {
            SaveLoadData.Load();
            SetCurrentLevel();
            
            SetTmpCostLvlUp();
            SetRandomSpriteAssetOnWorked();
        }
        
        private void SetCurrentLevel()
        {
            var levelId = SaveLoadData.LoadEmployeeWorker(_id);
            _currentEmployeeLevel = _employeeLevels[levelId];
            _desktopRenderer.material = _currentEmployeeLevel.Material;
            SetTmpCostLvlUp();
            
            _currentAssetsOnWorked = SaveLoadData.LoadCurrentAssetsOnWorkedKey(_id);
            _tmpMaxAssets.text = $"{_currentAssetsOnWorked}";
            
            var isBug = SaveLoadData.LoadCurrentIsBug(_id);
            if (isBug) SetBugActions();
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            GeneralInputReader.ClickAction += OnClickAction;
            GeneralInputReader.PdgClickAction += OnPdgClickAction;
            _playerInputs.ClickGameObject += OnClickGameObject;
            _playerInputs.MouseLeftClickAction += OnMouseLeftClickAction;
            GeneralInputReader.DevEarnMoneyAction += OnDevEarnMoneyAction;
            
            StartCoroutines();
        }
        
        private void OnDisable()
        {
            GeneralInputReader.ClickAction -= OnClickAction;
            GeneralInputReader.PdgClickAction -= OnPdgClickAction;
            _playerInputs.ClickGameObject -= OnClickGameObject;
            _playerInputs.MouseLeftClickAction -= OnMouseLeftClickAction;
            GeneralInputReader.DevEarnMoneyAction -= OnDevEarnMoneyAction;
        }

        private void Update()
        {
            if (_isPaused) return;
            
            PieceIncrement(_currentEmployeeLevel.IncrementAmount * Time.deltaTime);
            TryIncrementAssets();
            
            CheckMaxAssets();
            CheckIsBugAnimation();
        }

        #endregion
        
        #region Coroutines
        
        private void StartCoroutines()
        {
            StartCoroutine(IncrementFansAndMoneyAllTime());
            StartCoroutine(InfiniteCoroutine());
        }

        private IEnumerator IncrementFansAndMoneyAllTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(10f);
                
                var amountFans = (int)((float)_gameManager.TotalAssets / 100);
                var amountMoney = amountFans * _currentEmployeeLevel.MoneyGainAmout;
                
                IncrementFansAndMoney(amountFans, amountMoney);
            }
            
        }
        
        private IEnumerator InfiniteCoroutine()
        {
            var minTimeToBug = 15;
            var maxTimeToBug = 30;
                
            try
            {
                minTimeToBug = _objectiveManager.CurrentObjective.MinTimeToBug;
                maxTimeToBug = _objectiveManager.CurrentObjective.MaxTimeToBug;
            }
            catch (Exception)
            {
                // ignored
            }

            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minTimeToBug, maxTimeToBug));
                
                if (_isBug) continue;
                
                if (ChanceFunction())
                {
                    SetBugActions();
                }
            }
        }

        #endregion
        
        #region Functions
        
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
            
            if (_isPaused && !_isBug)
            {
                _isPaused = false;
                _employeeAnimator.SetTrigger(Stop);
            }
        }

        private void OnClickAction()
        {
            if (_isBug || GeneralInputReader.MenuValue) return;
            
            AnimatorSetSpeed(GameManager.SpeedBoost);
            PieceIncrement(_currentEmployeeLevel.IncrementClickAmount);
            StartCoroutine(ResetSpeed());
        }
        
        private void OnPdgClickAction()
        {
            if (_isBug || GeneralInputReader.MenuValue) return;
            
            AddAssetsOnWorked();
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
            if (_isBug)
            {
                SetCorrectionBug();
                return;
            }
            
            AddAssetsOnWorked();
        }
        
        private bool ChanceFunction()
        {
            if (_isPaused) return false;
            
            return Random.value <= _currentEmployeeLevel.ChanceBug;
        }

        private void CheckIsBugAnimation()
        {
            if (_isBug && !_employeeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pause"))
            {
                _employeeAnimator.SetTrigger(Pause);
            }
        }

        private void SetBugActions()
        {
            _isBug = true;
            _isPaused = true;
            _prefabBug.SetActive(true);
            _prefabBugButtonLvl.SetActive(false);
            _outlinableBug.enabled = true;
            
            _employeeAnimator.SetTrigger(Pause);
            _spriteProgressStop.fillAmount = 1;
            _tmpMaxAssets.text = "BUG";
            
            SaveLoadData.SaveCurrentIsBug(_id, true);
        }

        private void SetCorrectionBug()
        {
            _isBug = false;
            _isPaused = false;
            _prefabBug.SetActive(false);
            _prefabBugButtonLvl.SetActive(true);
            _outlinableBug.enabled = false;

            _employeeAnimator.SetTrigger(Stop);
            _spriteProgressStop.fillAmount = 0;
            _tmpMaxAssets.text = _currentAssetsOnWorked.ToString();
            
            SaveLoadData.SaveCurrentIsBug(_id, false);
        }
        
        private void AddAssetsOnWorked()
        {
            if (_currentAssetsOnWorked < 1) return;
            
            _gameManager.IncrementAssets(_currentAssetsOnWorked);
            
            SetRandomSpriteAssetOnWorked();
            IncrementFansAndMoney();
            ResetAll();

            SaveLoadData.Save();
        }

        private void IncrementFansAndMoney()
        {
            var amountFans = (int)((_currentAssetsOnWorked + (float)_gameManager.TotalAssets / 10) / 100 * _currentEmployeeLevel.FansGainAmout);
            if (amountFans <= 0) amountFans = 1;
            
            var amountMoney = _currentAssetsOnWorked * _currentEmployeeLevel.MoneyGainAmout;
            
            IncrementFansAndMoney(amountFans, amountMoney);
        }
        private static void IncrementFansAndMoney(int amountFans, int amountMoney)
        {
            _gameManager.IncrementFans(amountFans);
            _gameManager.IncrementMoney(amountMoney);
        }
        
        public void CheckLevelUp()
        {
            if (_currentEmployeeLevel.Level >= _employeeLevels.Length) return;
            if (_gameManager.Money < _employeeLevels[_currentEmployeeLevel.Level].CostLevel) return;
            
            LevelUp();
        }
        
        private void LevelUp()
        {
            _currentEmployeeLevel = _employeeLevels[_currentEmployeeLevel.Level];
            _desktopRenderer.material = _currentEmployeeLevel.Material;
            _gameManager.Money -= _currentEmployeeLevel.CostLevel;
            
            SetTmpCostLvlUp();
            SaveLoadData.Save();
            SaveLoadData.SaveEmployeeWorker(_id, _currentEmployeeLevel.Level);

            if (_currentAssetsOnWorked >= 1) 
                AddAssetsOnWorked();
        }

        private void SetTmpCostLvlUp()
        {
            _levelButtonManager.SetButtonLvl(_currentEmployeeLevel);
            
            if (_currentEmployeeLevel.Level >= _employeeLevels.Length)
            {
                _tmpCostLvlUp.text = "MAX";
                _prefabButtonLvlUp.SetActive(false);
                return;
            }
            
            var id = _currentEmployeeLevel.Level;
            _tmpCostLvlUp.text = $"{_employeeLevels[id].CostLevel}";
        }
        
        private void PieceIncrement(float incrementAmount)
        {
            _pieceInProgress += incrementAmount;
            SetSpriteProgress();
        }
        
        private void SetSpriteProgress()
        {
            _spriteProgress.fillAmount = _pieceInProgress / _objectiveManager.CurrentObjective.IncrementDelay;
        }
        
        private void TryIncrementAssets()
        {
            if(_pieceInProgress >= _objectiveManager.CurrentObjective.IncrementDelay)
            {
                IncrementCurrentAssetsOnWorked();
                ResetPieceInProgress();
                ResetSpriteProgress();
                SetRandomSpriteAssetOnWorked();
            }
        }

        private void IncrementCurrentAssetsOnWorked()
        {
            _tmpMaxAssets.text = $"{++_currentAssetsOnWorked}";
            SaveLoadData.SaveCurrentAssetsOnWorkedKey(_id, _currentAssetsOnWorked);
        }

        private void SetRandomSpriteAssetOnWorked()
        {
            var sprite = GetRandomSpriteAssetOnWorked();
            _spriteAssetOnWorked.sprite = sprite;
        }

        private static Sprite GetRandomSpriteAssetOnWorked()
        {
            var iconProps = _objectiveManager.CurrentObjective.IconProps.Icons;
            var iconLenght = iconProps.Length - 1;
            
            return iconProps[Random.Range(0, iconLenght)];
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
            AnimatorSetSpeed(_speedNormal);
        }

        #endregion

        #region Dev

        private static void OnDevEarnMoneyAction()
        {
            _gameManager.IncrementMoney(100000);
        }

        #endregion
    }
}
