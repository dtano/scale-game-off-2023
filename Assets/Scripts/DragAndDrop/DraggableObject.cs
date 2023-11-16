using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour
{
    [SerializeField] private LayerMask _eligibleForRaycastLayers;
    private Vector3 _offset;
    private Collider2D _collider;
    private Vector3 _originalPosition;

    public delegate void OnDragStartedDelegate();
    public event OnDragStartedDelegate OnDragStarted;

    public delegate void OnDragFailedDelegate();
    public event OnDragFailedDelegate OnDragFailed;


    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _originalPosition = transform.position;
    }

    bool IsDraggable()
    {
        return !GameStateManager.Instance.IsTabletOn && !GameStateManager.Instance.IsGameOver;
    }

    void OnMouseDown()
    {
        if (!IsDraggable()) return;

        _offset = transform.position - MouseToWorldPosition();
        OnDragStarted?.Invoke();
    }

    void OnMouseDrag()
    {
        if (!IsDraggable()) return;

        transform.position = MouseToWorldPosition() + _offset;
        Debug.DrawRay(Camera.main.transform.position, MouseToWorldPosition() - Camera.main.transform.position, Color.red);

        Vector2 rayOrigin = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        Vector3 mouseWorldPosition = MouseToWorldPosition();
        Vector2 rayDirection = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y) - rayOrigin;
        Debug.DrawRay(rayOrigin, rayDirection, Color.red);
    }

    void OnMouseUp()
    {
        if (!IsDraggable()) return;

        _collider.enabled = false;
        var rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hitInfo = Physics2D.Raycast(rayOrigin, Vector2.zero, _eligibleForRaycastLayers);
        Debug.DrawRay(new Vector2(rayOrigin.x, rayOrigin.y), Vector2.zero, Color.red);
        if (hitInfo && hitInfo.collider != null && hitInfo.collider.TryGetComponent(out DroppableArea droppableArea))
        {
            bool success = droppableArea.OnDropObject(this);
            if (!success) SetToOriginalPosition();
        }
        else
        {
            SetToOriginalPosition();
        }
        _collider.enabled = true;
    }

    private Vector3 MouseToWorldPosition()
    {
        var mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    public void ResetOriginalPosition()
    {
        _originalPosition = transform.position;
    }

    public void SetToOriginalPosition()
    {
        transform.position = _originalPosition;

        // Need to trigger some event
        OnDragFailed?.Invoke();
    }
}
