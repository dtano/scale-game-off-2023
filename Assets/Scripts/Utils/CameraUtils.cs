using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraUtils
{
    public static Vector3 MouseToWorldPosition(Transform transform)
    {
        var mouseScreenPosition = Input.mousePosition;
        Debug.Log("Z for mouse to world pos " + Camera.main.WorldToScreenPoint(transform.position).z);
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}
