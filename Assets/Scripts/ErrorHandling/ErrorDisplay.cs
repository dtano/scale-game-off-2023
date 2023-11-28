using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorDisplay : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _errorDisplayText;
    
    [SerializeField] private DragAndDropEventChannel _elevatorDragAndDropEventChannel;
    [SerializeField] private DragAndDropEventChannel _reservesDragAndDropEventChannel;
    [SerializeField] private ErrorDisplayEventChannel _errorDisplayEventChannel;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();

        if(_elevatorDragAndDropEventChannel != null)
        {
            _elevatorDragAndDropEventChannel.OnFailedDropEvent += ShowError;
        }
        
        if (_reservesDragAndDropEventChannel != null)
        {
            _reservesDragAndDropEventChannel.OnFailedDropEvent += ShowError;
        }

        if(_errorDisplayEventChannel != null)
        {
            _errorDisplayEventChannel.OnRequestErrorEvent += ShowError;
        }
    }

    public void ShowError(ErrorDTO errorDTO)
    {
        if (errorDTO == null || (errorDTO.Message == null || errorDTO.Message.Length == 0)) return;

        _errorDisplayText.text = errorDTO.Message;
        if (_animator != null) _animator.SetTrigger("ShowError");
    }

    private void UnregisterListeners()
    {
        if (_elevatorDragAndDropEventChannel != null)
        {
            _elevatorDragAndDropEventChannel.OnFailedDropEvent -= ShowError;
        }

        if (_reservesDragAndDropEventChannel != null)
        {
            _reservesDragAndDropEventChannel.OnFailedDropEvent -= ShowError;
        }

        if (_errorDisplayEventChannel != null)
        {
            _errorDisplayEventChannel.OnRequestErrorEvent -= ShowError;
        }
    }

    private void OnDestroy()
    {
        UnregisterListeners();
    }
}
