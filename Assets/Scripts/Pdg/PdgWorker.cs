using Audio;
using JetBrains.Annotations;
using PlayerController;
using UnityEngine;

namespace Pdg
{
    public class PdgWorker : MonoBehaviour
    {
        #region Statements

        private InputReader _playerInputs;
        
        private PdgWorker _pdgWorker;
        [CanBeNull] private PdgWorker _pdgWorkerClicked;
        
        private void Awake()
        {
            _playerInputs = GetComponent<InputReader>();
            _pdgWorker = GetComponent<PdgWorker>();
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
            _pdgWorkerClicked = clickedObject.TryGetComponent<PdgWorker>(out var pdgWorker)
                ? pdgWorker
                : null;
        }

        private void OnMouseLeftClickAction()
        {
            if (_pdgWorkerClicked != _pdgWorker) return;
            
            MusicManager.instance.MmfClick.PlayFeedbacks();
            GeneralInputReader.OnStaticPdgClickAction();
        }

        #endregion
    }
}
