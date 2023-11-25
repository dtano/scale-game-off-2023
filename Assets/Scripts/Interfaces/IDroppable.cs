using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDroppable
{
    DropResultDTO OnDropObject(DraggableObject draggableObject);
}
