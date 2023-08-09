using System;
using Audio;
using Employee;
using JetBrains.Annotations;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bug.MiniGame
{
    public class MiniGameManager : MonoBehaviour
    {
        #region Statements
        
        [FormerlySerializedAs("_mmfPlayer")]
        [Header("Feel")]
        [SerializeField] private MMF_Player _mmfPlayerInit;
        [SerializeField] private MMF_Player _mmfPlayerError;
        [SerializeField] private MMF_Player _mmfPlayerValid;

        public static Action<EmployeeWorker> BugAction { get; private set; }
        public static Action BugError { get; private set; }
        public static Action BugValid { get; private set; }
        [CanBeNull] public static Action<EmployeeWorker> BugCorrectedAction { get; set; }

        [CanBeNull] public static EmployeeWorker CurrentEmployeeWorker { get; set; }
        public static bool IsOnMiniGame { get; set; }
        public static bool IsOnHint { get; set; }
        
        [SerializeField] private GameObject[] _miniGameObjects;

        #endregion

        #region Events

        private void OnEnable()
        {
            BugAction += OnBugAction;
            BugError += OnBugError;
            BugValid += OnBugValid;
        }
        
        private void OnDisable()
        {
            BugAction -= OnBugAction;
            BugError -= OnBugError;
            BugValid -= OnBugValid;
        }

        #endregion

        #region Functions

        private void OnBugAction(EmployeeWorker employeeWorker)
        {
            _mmfPlayerInit.PlayFeedbacks();
            IsOnMiniGame = true;
            CurrentEmployeeWorker = employeeWorker;
            RandomMiniGame();
        }
        
        private void OnBugError()
        {
            _mmfPlayerError.PlayFeedbacks();
        }
        
        private void OnBugValid()
        {
            _mmfPlayerValid.PlayFeedbacks();
        }

        public static void ResetIsOnMiniGame()
        {
            IsOnMiniGame = false;
        }

        private void RandomMiniGame()
        {
            //_miniGameObjects[2].SetActive(true);
            _miniGameObjects[UnityEngine.Random.Range(0, _miniGameObjects.Length)].SetActive(true);
            MusicManager.instance.MmfSwip.PlayFeedbacks();
        }
        
        public static void AddFansAndMoney()
        {
            var _gameManager = GameManager.instance;
            var amoutFansGain = (int)(_gameManager.Fans * 0.01f);
            _gameManager.IncrementFans(amoutFansGain < 1 ? 1 : amoutFansGain);
        }

        #endregion
    }
}
