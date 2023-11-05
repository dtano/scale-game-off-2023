using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// How can I tell if the game is over?
public class BuildingController : MonoBehaviour
{
    [SerializeField] private BuildingDataSO _buildingData;
    [SerializeField] private EmployeeSpawner _employeeSpawner;
    [SerializeField] private ElevatorQueue _elevatorQueue;
    [SerializeField] private List<Elevator> _elevators;
    [SerializeField] private DragAndDropEventChannel _dragAndDropEventChannel;

    private Employee _currentFirstEmployee;

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

        // Then initiate the steps
        if (_employeeSpawner != null) _employeeSpawner.SpawnEmployees(_buildingData.NumFloors);

        foreach(Elevator elevator in _elevators){
            elevator.SetTotalFloors(_buildingData.NumFloors);
        }
        
        // Once employees are spawned, take out the first employee to be displayed in game
        _currentFirstEmployee = _elevatorQueue.GetNextInQueue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAddEmployeeToElevator(DraggableObject draggableObject)
    {
        if (draggableObject == null) return;

        if(draggableObject.TryGetComponent(out Employee employee))
        {
            _elevatorQueue.RemoveFromQueue(employee);
            _currentFirstEmployee = _elevatorQueue.GetNextInQueue();

            // Need to check if we've reached the end of the queue
            if (_currentFirstEmployee == null)
            {
                Debug.Log("Reached the end of queue");
            }
        }
    }
}
