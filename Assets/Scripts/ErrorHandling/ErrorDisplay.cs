using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorDisplay : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _errorDisplayText;
    
    // Damn I forgot I'm using 2 channels for drag and drop
    [SerializeField] private DragAndDropEventChannel _elevatorDragAndDropEventChannel;
    [SerializeField] private DragAndDropEventChannel _reservesDragAndDropEventChannel;

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
    }

    private void OnDestroy()
    {
        UnregisterListeners();
    }
}
