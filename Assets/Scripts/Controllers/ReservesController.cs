using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservesController : MonoBehaviour
{
    [SerializeField] private ElevatorQueue _reservesQueue;
    [SerializeField] private DragAndDropEventChannel _channel;
    [SerializeField] private GameObject _employeeParent;
    [SerializeField] private Transform _employeePositionMarker;

    private Employee _currentFirstEmployee;

    // Start is called before the first frame update
    void Start()
    {
        if (_reservesQueue == null) _reservesQueue = GetComponent<ElevatorQueue>();

        if(_channel != null)
        {
            _channel.OnSuccessfulDropEvent += OnAddEmployeeToReserves;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAddEmployeeToReserves(DraggableObject draggableObject)
    {
        if(draggableObject.TryGetComponent(out Employee employee))
        {
            AddToQueue(employee);
        }
    }

    public bool AddToQueue(Employee employee)
    {
        if(employee == null)
        {
            Debug.Log("Tried to add null to queue");
            return false;
        }

        bool successfullyAddToQueue = _reservesQueue.AddToQueue(employee);
        if (!successfullyAddToQueue)
        {
            Debug.Log("Failed to add");
            return false;
        }

        // Change the parent of the given object
        employee.gameObject.transform.SetParent(_employeeParent.transform);
        employee.gameObject.transform.position = _employeePositionMarker.position;
        if (_currentFirstEmployee == null)
        {
            employee.gameObject.SetActive(true);
            Debug.Log("Set employee object active");
            _currentFirstEmployee = employee;
        }


        return true;
    }
}
