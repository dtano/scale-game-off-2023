using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour
{
    private Vector3 _offset;
    private Collider2D _collider;
    private Vector3 _originalPosition;


    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        _offset = transform.position - MouseToWorldPosition();
    }

    void OnMouseDrag()
    {
        transform.position = MouseToWorldPosition() + _offset;
        Debug.DrawRay(Camera.main.transform.position, MouseToWorldPosition() - Camera.main.transform.position, Color.red);

        Vector2 rayOrigin = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        Vector3 mouseWorldPosition = MouseToWorldPosition();
        Vector2 rayDirection = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y) - rayOrigin;
        Debug.DrawRay(rayOrigin, rayDirection, Color.red);
    }

    void OnMouseUp()
    {
        _collider.enabled = false;
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseToWorldPosition() - rayOrigin;
        Debug.Log("Ray origin " + rayOrigin);
        Debug.Log("Ray direction " + rayDirection);

        //RaycastHit raycastHit;
        //Debug.DrawRay(rayOrigin, rayDirection, Color.red);
        //if (Physics.Raycast(rayOrigin, rayDirection, out raycastHit))
        //{
        //    Debug.Log("Hit " + raycastHit.collider.gameObject.name);
        //    if (raycastHit.collider.TryGetComponent(out DroppableArea droppable))
        //    {
        //        Debug.Log("Dropped in a droppable area " + raycastHit.collider.name);
        //    }
        //    else
        //    {
        //        transform.position = _originalPosition;
        //    }
        //}
        //else
        //{
        //    transform.position = _originalPosition;
        //}

        RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, rayDirection);
        Debug.DrawRay(new Vector2(rayOrigin.x, rayOrigin.y), new Vector2(rayDirection.x, rayDirection.y), Color.red);
        if (hitInfo && hitInfo.collider != null && hitInfo.collider.TryGetComponent(out DroppableArea droppableArea))
        {
            Debug.Log("Dropped in a droppable area " + hitInfo.collider.name);
            Debug.Log("Hit at " + hitInfo.point);
        }
        else
        {
            transform.position = _originalPosition;
        }
        _collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 MouseToWorldPosition()
    {
        var mouseScreenPosition = Input.mousePosition;
        //Debug.Log("World to screen point " + Camera.main.WorldToScreenPoint(transform.position));
        //Debug.Log("Z for mouse to world pos " + Camera.main.WorldToScreenPoint(transform.position).z);
        //Debug.Log("Mouse screen position " + mouseScreenPosition);
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    public void ResetOriginalPosition()
    {
        _originalPosition = transform.position;
    }
}
