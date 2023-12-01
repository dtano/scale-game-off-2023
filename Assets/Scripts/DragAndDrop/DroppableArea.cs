using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableArea : MonoBehaviour
{
    [SerializeField] private DragAndDropEventChannel eventChannel;
    [SerializeField] private AudioEventChannel audioChannel;
    [SerializeField] private AudioCueSO successfulDropSfx;
    [SerializeField] private AudioCueSO failedDropSfx;
    public delegate DropResultDTO OnDropObjectDelegate(DraggableObject draggableObject);
    public event OnDropObjectDelegate OnDropObjectEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool OnDropObject(DraggableObject draggableObject)
    {        
        bool success = false;
        DropResultDTO result = OnDropObjectEvent?.Invoke(draggableObject);

        if(result != null)
        {
            success = result.Success;
            if (success)
            {
                eventChannel.OnSuccessfulDrop(draggableObject);
                if (audioChannel != null) audioChannel.RaiseEvent(successfulDropSfx);
            }
            else
            {
                eventChannel.OnFailedDrop(result.Error);
                if (audioChannel != null) audioChannel.RaiseEvent(failedDropSfx);
            }
        }

        return success;
    }
}
