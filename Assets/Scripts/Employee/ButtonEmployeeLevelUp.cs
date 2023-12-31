using Audio;
using JetBrains.Annotations;
using MoreMountains.Feedbacks;
using PlayerController;
using UnityEngine;

namespace Employee
{
    public class ButtonEmployeeLevelUp : MonoBehaviour
    {
        #region Statements

        [Header("Feel")]
        [SerializeField] private MMF_Player _mmfPlayer;
        
        [SerializeField] private InputReader _playerInputs;
        [SerializeField] private EmployeeWorker _employeeWorker;
        
        private ButtonEmployeeLevelUp _buttonEmployeeLevelUp;
        [CanBeNull] private ButtonEmployeeLevelUp _buttonEmployeeLevelUpClicked;
        
        private void Awake()
        {
            _buttonEmployeeLevelUp = GetComponent<ButtonEmployeeLevelUp>();
        }

        #endregion
    
        #region Events

        private void OnEnable()
        {
            _playerInputs.ClickGameObject += OnClickGameObject;
            _playerInputs.MouseLeftClickAction += OnMouseLeftClickAction;
        }
            
        private void OnDisable()
        {
            _playerInputs.ClickGameObject -= OnClickGameObject;
            _playerInputs.MouseLeftClickAction -= OnMouseLeftClickAction;
        }

        #endregion

        #region Functions

        private void OnClickGameObject(GameObject clickedObject)
        {
            _buttonEmployeeLevelUpClicked = clickedObject.TryGetComponent<ButtonEmployeeLevelUp>(out var buttonEmployeeLevelUp)
                ? buttonEmployeeLevelUp
                : null;
        }

        private void OnMouseLeftClickAction()
        {
            if (_buttonEmployeeLevelUpClicked != _buttonEmployeeLevelUp) return;
            
            _mmfPlayer.PlayFeedbacks();
            MusicManager.instance.MmfClick.PlayFeedbacks();
            
            _employeeWorker.CheckLevelUp();
            _buttonEmployeeLevelUpClicked = null;
        }

        #endregion
    }
}
