using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableArea : MonoBehaviour
{
    [SerializeField] private DragAndDropEventChannel eventChannel;
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
            if (result.Success)
            {
                eventChannel.OnSuccessfulDrop(draggableObject);
            }
            else
            {
                eventChannel.OnFailedDrop(result.Error);
            }
        }

        return success;
    }
}
