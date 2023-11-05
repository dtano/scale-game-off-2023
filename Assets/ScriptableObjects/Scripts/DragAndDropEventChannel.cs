using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drag and Drop Event Channel", menuName = "Scriptable Objects/Event Channels/Drag and Drop")]
public class DragAndDropEventChannel : ScriptableObject
{
    public delegate void OnSuccessfulDropDelegate(DraggableObject draggableObject);
    public event OnSuccessfulDropDelegate OnSuccessfulDropEvent;

    public delegate void OnFailedDropDelegate(DraggableObject draggableObject);
    public event OnFailedDropDelegate OnFailedDropEvent;

    public delegate void OnStartDragDelegate(DraggableObject draggableObject);
    public event OnStartDragDelegate OnStartDragEvent;

    public void OnSuccessfulDrop(DraggableObject draggableObject)
    {
        if(OnSuccessfulDropEvent != null)
        {
            OnSuccessfulDropEvent.Invoke(draggableObject);
        }
    }

    public void OnFailedDrop(DraggableObject draggableObject)
    {
        if(OnFailedDropEvent != null)
        {
            OnFailedDropEvent.Invoke(draggableObject);
        }
    }

    public void OnStartDrag(DraggableObject draggableObject)
    {
        if(OnStartDragEvent != null)
        {
            OnStartDragEvent.Invoke(draggableObject);
        }
    }
}
