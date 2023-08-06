using System.Collections;
using EPOOutline;
using JetBrains.Annotations;
using PlayerController;
using SaveData;
using ScriptableOject.EmployeeLevel;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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
        
        [SerializeField] private GameObject _prefabBug;
        private Outlinable _outlinable;
        private bool _isBug;

        private void Awake()
        {
            _playerInputs = GetComponent<InputReader>();
            _employeeWorker = GetComponent<EmployeeWorker>();
            _outlinable = GetComponent<Outlinable>();
        }

        private void Start()
        {
            SaveLoadData.Load();
            SetCurrentLevel();
            
            SetTmpCostLvlUp();
            
            StartCoroutine(IncrementFansAndMoneyAllTime());
            StartCoroutine(InfiniteCoroutine());
            
            SetRandomSpriteAssetOnWorked();
        }
        
        private void SetCurrentLevel()
        {
            var levelId = SaveLoadData.LoadEmployeeWorker(GetInstanceID());
            _currentEmployeeLevel = _employeeLevels[levelId];
            _desktopRenderer.material = _currentEmployeeLevel.Material;
            SetTmpCostLvlUp();
            
            _currentAssetsOnWorked = SaveLoadData.LoadCurrentAssetsOnWorkedKey(GetInstanceID());
            _tmpMaxAssets.text = $"{_currentAssetsOnWorked}";
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            _playerInputs.ClickAction += OnClickAction;
            InputReader.PdgClickAction += OnPdgClickAction;
            _playerInputs.ClickGameObject += OnClickGameObject;
            _playerInputs.MouseLeftClickAction += OnMouseLeftClickAction;
            _playerInputs.DevEarnMoneyAction += OnDevEarnMoneyAction;
        }
        
        private void OnDisable()
        {
            _playerInputs.ClickAction -= OnClickAction;
            InputReader.PdgClickAction -= OnPdgClickAction;
            _playerInputs.ClickGameObject -= OnClickGameObject;
            _playerInputs.MouseLeftClickAction -= OnMouseLeftClickAction;
            _playerInputs.DevEarnMoneyAction -= OnDevEarnMoneyAction;
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
            CheckIsBugAnimation();
        }

        #endregion
        
        #region Coroutines

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
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(3, 6));
                
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
            if (_isBug) return;
            
            AnimatorSetSpeed(GameManager.SpeedBoost);
            PieceIncrement(_currentEmployeeLevel.IncrementClickAmount);
            StartCoroutine(ResetSpeed());
        }
        
        private void OnPdgClickAction()
        {
            if (_isBug) return;
            
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
            
            return Random.value <= 0.9f;
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

            SetOutlinableBugColor();
            
            _employeeAnimator.SetTrigger(Pause);
            _spriteProgressStop.fillAmount = 1;
            _tmpMaxAssets.text = "BUG";
        }

        private void SetCorrectionBug()
        {
            _isBug = false;
            _isPaused = false;
            _prefabBug.SetActive(false);

            SetOutlinableColor();
            _employeeAnimator.SetTrigger(Stop);
            _spriteProgressStop.fillAmount = 0;
            _tmpMaxAssets.text = _currentAssetsOnWorked.ToString();
        }

        private void SetOutlinableColor()
        {
            _outlinable.OutlineParameters.Color = new Color(1f, 1f, 1f, 1f);
        }
        private void SetOutlinableBugColor()
        {
            _outlinable.OutlineParameters.Color = new Color(1f, 0.1f, 0.016f, 1f);
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
            SaveLoadData.SaveEmployeeWorker(GetInstanceID(), _currentEmployeeLevel.Level);

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
            SaveLoadData.SaveCurrentAssetsOnWorkedKey(GetInstanceID(), _currentAssetsOnWorked);
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
            _gameManager.IncrementMoney(10000);
        }

        #endregion
    }
}
