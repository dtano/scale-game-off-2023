using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType
{
    Tiny,
    Small,
    Medium,
    Large,
    Massive
}

public enum QueueType
{
    Elevator,
    Reserves
}

public class Employee : MonoBehaviour
{
    [SerializeField] private Guid _id;
    [SerializeField] private int _weight;
    [SerializeField] private int _destinationFloor;
    [SerializeField] private float _satisfactionLevel = 100f;
    [SerializeField] private BodyType _bodyType;
    [SerializeField] private EmployeeInfoUI _employeeInfoUI;

    private SpriteRenderer _spriteRenderer;
    private DraggableObject _draggableComponent;
    private QueueType _currentQueueType = QueueType.Elevator;
    private int _currentQueuePosition;
    private int _originalQueuePosition;

    public Guid Id => _id;
    public int Weight => _weight;
    public int DestinationFloor => _destinationFloor;
    public float SatisfactionLevel => _satisfactionLevel;
    public BodyType BodyType => _bodyType;
    public EmployeeInfoUI EmployeeInfoUI => _employeeInfoUI;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public DraggableObject DraggableComponent => _draggableComponent;
    public QueueType CurrentQueueType { get => _currentQueueType; set => _currentQueueType = value; }
    public int CurrentQueuePosition { get => _currentQueuePosition; set => _currentQueuePosition = value; }
    public int OriginalQueuePosition { get => _originalQueuePosition; set => _originalQueuePosition = value; }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _draggableComponent = GetComponent<DraggableObject>();
        if(_draggableComponent != null)
        {
            _draggableComponent.OnDragStarted += OnDrag;
            _draggableComponent.OnDragFailed += OnDragFailed;
        }
    }

    private void OnDrag()
    {
        if (_employeeInfoUI != null) _employeeInfoUI.Hide();
    }

    private void OnDragFailed()
    {
        if (_employeeInfoUI != null) _employeeInfoUI.Show();
    }

    void Update()
    {
        
    }

    // Sprite might also need to be added here
    public void SetEmployeeData(Guid id, BodyType bodyType, int weight, int destinationFloor, Sprite sprite, int currentQueuePosition = 0, float satisfactionLevel = 100f)
    {
        _id = id;
        _bodyType = bodyType;
        _weight = weight;
        _destinationFloor = destinationFloor;
        _satisfactionLevel = satisfactionLevel;

        _originalQueuePosition = currentQueuePosition;
        _currentQueuePosition = currentQueuePosition;

        if(_employeeInfoUI != null) _employeeInfoUI.SetInformation(weight, destinationFloor);
        if (sprite != null) _spriteRenderer.sprite = sprite;

        // Need to trigger some sort of fade in animation so that the player can tell that the employee has changed
    }

    public override bool Equals(object obj)
    {
        var other = obj as Employee;

        if (other == null)
        {
            return false;
        }

        return _id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}
