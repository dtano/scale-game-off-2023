using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDroppable
{
    bool OnDropObject(DraggableObject draggableObject);
}
