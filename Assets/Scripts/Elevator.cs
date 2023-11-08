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
    [SerializeField] private FloorNumberIndicator _floorNumberIndicator;

    private DroppableArea droppableArea;
    private Dictionary<int, List<Employee>> _destinationMap = new Dictionary<int, List<Employee>>();
    
    private bool _isMoving = false;
    private int _currentCapacity;
    private int _currentFloor;
    private int _totalFloors;

    public HashSet<Employee> Passengers => _passengers;
    public Dictionary<int, List<Employee>> DestinationMap => _destinationMap;
    public string Name => _elevatorData.Model;
    public int CurrentCapacity => _currentCapacity;
    public int MaxCapacity => _elevatorData.MaxCapacity;

    // Start is called before the first frame update
    void Awake()
    {
        droppableArea = GetComponent<DroppableArea>();
        droppableArea.OnDropObjectEvent += OnDropObject;

        if(_elevatorDisplayUI != null) _elevatorDisplayUI.SetInformation(_currentCapacity, _elevatorData.MaxCapacity);
        if(_floorNumberIndicator != null) _floorNumberIndicator.SetFloorNumber(_currentFloor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CanAddToElevator(int weightToAdd)
    {
        return !_isMoving && (_currentCapacity + weightToAdd < _elevatorData.MaxCapacity);
    }

    public void InitiateMovement()
    {
        _isMoving = true;
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

    private void ReleasePassengersAtGivenFloor(int floorNumber)
    {
        if (!_destinationMap.ContainsKey(floorNumber)) return;

        List<Employee> passengersToRelease = new List<Employee>(_destinationMap[floorNumber]);
        foreach (Employee passenger in passengersToRelease)
        {
            RemoveFromElevator(passenger);
            Destroy(passenger.gameObject);
        }

        _destinationMap[floorNumber].Clear();
        _destinationMap.Remove(floorNumber);
    }

    private void OnReachedFloor(int floorNumber)
    {
        // Play some sort of sound
        ReleasePassengersAtGivenFloor(floorNumber);
    }

    public void StartJourney()
    {
        if (_isMoving) return;
        if(_currentDirection == Direction.Up && _passengers.Count == 0)
        {
            Debug.Log("Can't start since lift is empty");
            return;
        }

        if (_currentDirection == Direction.Down && _currentFloor == 0) return;

        Debug.Log("Starting Journey");
        if (_elevatorDisplayUI != null) _elevatorDisplayUI.Hide();
        _isMoving = true;

        StartCoroutine(ProcessTrip());
    }

    private void OnElevatorReturn()
    {
        // Trigger Open elevator animation
        if (_elevatorDisplayUI != null) _elevatorDisplayUI.Show();
        _isMoving = false;
        _currentDirection = Direction.Up;
    }

    private void OnReachTop()
    {
        _currentDirection = Direction.Down;
        StartCoroutine(ProcessTrip());
    }

    private IEnumerator ProcessTrip()
    {
        if(_currentDirection == Direction.Up)
        {
            while (_currentFloor != _totalFloors)
            {
                Debug.Log($"Currently at floor {_currentFloor}");
                // After 2 seconds the elevator has reached a floor
                if (_destinationMap.ContainsKey(_currentFloor))
                {
                    Debug.Log($"Stopping at floor {_currentFloor}");
                    OnReachedFloor(_currentFloor);
                    yield return new WaitForSeconds(1f);
                    Debug.Log("Moving On...");
                }
                
                if (_destinationMap.Keys.Count == 0)
                {
                    // Means that all of the destination floors have been visited
                    break;
                }

                yield return new WaitForSeconds(2f); // This time depends on the elevator type
                _currentFloor++;
                if (_floorNumberIndicator != null) _floorNumberIndicator.SetFloorNumber(_currentFloor);
            }

            OnReachTop();
        }
        else if(_currentDirection == Direction.Down)
        {
            while(_currentFloor != 0)
            {
                yield return new WaitForSeconds(2f);
                _currentFloor--;
                if (_floorNumberIndicator != null) _floorNumberIndicator.SetFloorNumber(_currentFloor);
            }

            // Once we've reached the bottom floor, trigger some function
            OnElevatorReturn();
        }

        Debug.Log("Trip Over");
    }

    public bool OnDropObject(DraggableObject draggableObject)
    {
        if (_isMoving) return false;
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

    public void SetTotalFloors(int totalFloors)
    {
        _totalFloors = totalFloors;
    }
}
