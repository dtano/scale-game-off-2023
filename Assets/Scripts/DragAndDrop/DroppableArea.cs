using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableArea : MonoBehaviour
{
    [SerializeField] private DragAndDropEventChannel eventChannel;
    public delegate bool OnDropObjectDelegate(DraggableObject draggableObject);
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
        Debug.Log("Dropped object in " + gameObject.name);
        bool success = OnDropObjectEvent?.Invoke(draggableObject) ?? false;

        if (success)
        {
            Debug.Log("Call on succesful drop");
            eventChannel.OnSuccessfulDrop(draggableObject);
        }
        else
        {
            eventChannel.OnFailedDrop(draggableObject);
        }

        return success;
    }
}
