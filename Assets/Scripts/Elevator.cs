using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down
}

public class Elevator : MonoBehaviour, IDroppable
{
    [SerializeField] private ElevatorDataSO _elevatorData;
    [SerializeField] private HashSet<Employee> _passengers = new HashSet<Employee>();
    [SerializeField] private Direction _currentDirection = Direction.Up;
    [SerializeField] private ElevatorDisplayUI _elevatorDisplayUI;

    private DroppableArea droppableArea;
    private int _currentCapacity;
    private Dictionary<int, List<Employee>> _destinationMap = new Dictionary<int, List<Employee>>();
    private bool _shouldMove = false;

    public HashSet<Employee> Passengers => _passengers;

    // Start is called before the first frame update
    void Awake()
    {
        droppableArea = GetComponent<DroppableArea>();
        droppableArea.OnDropObjectEvent += OnDropObject;

        if(_elevatorDisplayUI != null) _elevatorDisplayUI.SetInformation(_currentCapacity, _elevatorData.MaxCapacity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CanAddToElevator(int weightToAdd)
    {
        return _currentCapacity + weightToAdd < _elevatorData.MaxCapacity;
    }

    public void InitiateMovement()
    {
        _shouldMove = true;
    }

    // Before calling this, the employee must be removed from whatever queue they were in
    public bool AddToElevator(Employee employee)
    {
        // Check weight here
        if (!CanAddToElevator(employee.Weight))
        {
            Debug.Log($"Failed to add since {_currentCapacity + employee.Weight} > {_elevatorData.MaxCapacity}");
            return false;
        }

        bool success = _passengers.Add(employee);
        if (!success)
        {
            Debug.Log("Fail to add employee to elevator");
            return false;
        }

        // Map it in the destinationMap
        int destination = employee.DestinationFloor;
        if (_destinationMap.ContainsKey(destination))
        {
            _destinationMap[destination].Add(employee);
        }
        else
        {
            _destinationMap.Add(destination, new List<Employee>());
            _destinationMap[destination].Add(employee);
        }

        _currentCapacity += employee.Weight;
        if (_elevatorDisplayUI != null) _elevatorDisplayUI.SetInformation(_currentCapacity, _elevatorData.MaxCapacity);

        return true;
    }

    public void RemoveFromElevator(Employee employee)
    {
        bool success = _passengers.Remove(employee);
        if (!success)
        {
            Debug.Log("Fail to remove employee to elevator");
            return;
        }

        if (_destinationMap.ContainsKey(employee.DestinationFloor))
        {
            _destinationMap[employee.DestinationFloor].Remove(employee);
        }

        _currentCapacity -= employee.Weight;
        if (_elevatorDisplayUI != null) _elevatorDisplayUI.SetInformation(_currentCapacity, _elevatorData.MaxCapacity);
    }

    public void StartJourney()
    {
        if(_passengers.Count == 0)
        {
            Debug.Log("Can't start since lift is empty");
            return;
        }

        Debug.Log("Starting Journey");
        if (_elevatorDisplayUI != null) _elevatorDisplayUI.Hide();

    }

    public bool OnDropObject(DraggableObject draggableObject)
    {
        // Need to get component from draggableObject
        if (draggableObject == null) return false;

        if(draggableObject.gameObject.TryGetComponent(out Employee employee))
        {
            Debug.Log("Dropping employee in elevator");
            bool successfulAddition = AddToElevator(employee);

            if (successfulAddition)
            {
                // Hide the employee once they are added to the elevator
                draggableObject.gameObject.SetActive(false);
            }

            return successfulAddition;
        }

        return false;
    }
}
