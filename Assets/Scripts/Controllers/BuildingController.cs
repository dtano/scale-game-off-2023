using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// How can I tell if the game is over?
public class BuildingController : MonoBehaviour
{
    [SerializeField] private BuildingDataSO _buildingData;
    [SerializeField] private EmployeeSpawner _employeeSpawner;
    [SerializeField] private ReservesController _reservesController;
    [SerializeField] private ElevatorQueue _elevatorQueue;
    [SerializeField] private List<Elevator> _elevators;

    [SerializeField] private DragAndDropEventChannel _dragAndDropEventChannel;
    [SerializeField] private DragAndDropEventChannel _reservesDragAndDropEventChannel;
    [SerializeField] private TabletInteractionEventChannel _tabletInteractionEventChannel;
    [SerializeField] private EmployeePaginationUI _elevatorQueueUI;

    private Employee _currentFirstEmployee;

    public List<Elevator> Elevators => _elevators;

    // Start is called before the first frame update
    void Start()
    {
        if(_elevatorQueue == null)
        {
            _elevatorQueue = GetComponent<ElevatorQueue>();
        }

        if (_dragAndDropEventChannel != null)
        {
            _dragAndDropEventChannel.OnSuccessfulDropEvent += OnAddEmployeeToElevator;
        }

        if(_reservesDragAndDropEventChannel != null)
        {
            _reservesDragAndDropEventChannel.OnSuccessfulDropEvent += OnDropEmployeeInReserves;
        }

        if(_tabletInteractionEventChannel != null)
        {
            _tabletInteractionEventChannel.OnKickEmployeeFromElevatorEvent += OnKickEmployeeFromElevator;
        }

        // Then initiate the steps
        if (_employeeSpawner != null) _employeeSpawner.SpawnEmployees(_buildingData.NumFloors);

        foreach(Elevator elevator in _elevators){
            elevator.SetTotalFloors(_buildingData.NumFloors);
        }
        
        // Once employees are spawned, take out the first employee to be displayed in game
        _currentFirstEmployee = _elevatorQueue.GetNextInQueue();

        if(_elevatorQueueUI != null)
        {
            _elevatorQueueUI.OnClickArrowEvent += UpdateElevatorQueueUI;
            UpdateElevatorQueueUI();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAddEmployeeToElevator(DraggableObject draggableObject)
    {
        Debug.Log("OnAddEmployeeToElevator");
        if (draggableObject == null) return;

        if(draggableObject.TryGetComponent(out Employee employee))
        {
            // Need to check where the employee was dragged from
            if (QueueType.Elevator == employee.CurrentQueueType)
            {
                bool removalSuccess = _elevatorQueue.RemoveFromQueue(employee);
                if (!removalSuccess) Debug.Log("Failed to remove an employee from the elevatorQueue");
                _currentFirstEmployee = _elevatorQueue.GetNextInQueue();

                UpdateElevatorQueueUI();
            }
            else
            {
                _reservesController.RemoveEmployee(employee);
            }

            // Need to check if we've reached the end of the queue
            if (_currentFirstEmployee == null)
            {
                Debug.Log("Reached the end of queue");
            }
        }
    }

    // I think I can find a way to use just one event channel for this
    private void OnDropEmployeeInReserves(DraggableObject draggableObject)
    {
        // Remove from elevator queue
        // Get next employee in queue
        // 
        Debug.Log("OnDropEmployeeInReserves");
        if (draggableObject == null) return;

        if (draggableObject.TryGetComponent(out Employee employee))
        {
            _elevatorQueue.RemoveFromQueue(employee);
            _currentFirstEmployee = _elevatorQueue.GetNextInQueue();
            UpdateElevatorQueueUI();
        }
    }

    private void UpdateElevatorQueueUI()
    {
        if (_elevatorQueueUI != null)
        {
            List<Employee> employeesToDisplay = new List<Employee>();
            for(int i = 1; i < _elevatorQueue.Count; i++)
            {
                employeesToDisplay.Add(_elevatorQueue.GetByIndex(i));
            }
            _elevatorQueueUI.UpdateView(employeesToDisplay, null);
        }
    }

    private void OnKickEmployeeFromElevator(Elevator elevator, Employee employee)
    {
        // Remove from elevator
        elevator.RemoveFromElevator(employee);

        // Add to reserves
        _reservesController.AddToQueue(employee);
    }
}
