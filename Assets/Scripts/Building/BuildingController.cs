using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private BuildingDataSO _buildingData;
    [SerializeField] private EmployeeSpawner _employeeSpawner;
    [SerializeField] private ElevatorQueue _elevatorQueue;
    [SerializeField] private List<Elevator> _elevators;

    // Start is called before the first frame update
    void Start()
    {
        if(_elevatorQueue == null)
        {
            _elevatorQueue = GetComponent<ElevatorQueue>();
        }

        // Then initiate the steps
        if (_employeeSpawner != null) _employeeSpawner.SpawnEmployees(_buildingData.NumFloors);
        
        // Once employees are spawned, take out the first employee to be displayed in game
        _elevatorQueue.GetNextInQueue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
