using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReservesController : MonoBehaviour
{
    [SerializeField] private ElevatorQueue _reservesQueue;
    [SerializeField] private ReservesUI _reservesUI;
    [SerializeField] private TextMeshProUGUI _queueCountText;
    [SerializeField] private DragAndDropEventChannel _channel;
    [SerializeField] private GameObject _employeeParent;
    [SerializeField] private Transform _employeePositionMarker;

    [SerializeField] private DroppableArea _droppableArea;
    private Employee _currentDisplayedEmployee;

    // Start is called before the first frame update
    void Start()
    {
        if (_reservesQueue == null) _reservesQueue = GetComponent<ElevatorQueue>();

        _droppableArea = GetComponent<DroppableArea>();
        if(_droppableArea != null)
        {
            _droppableArea.OnDropObjectEvent += OnDropEmployee;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool OnDropEmployee(DraggableObject draggableObject)
    {
        Debug.Log("On drop employee in reserves");
        // Need to somehow prevent the employee from being readded
        if (draggableObject.TryGetComponent(out Employee employee))
        {
            Debug.Log("Checking employee queue type " + employee.CurrentQueueType);
            if (QueueType.Reserves == employee.CurrentQueueType)
            {
                Debug.Log("Tried to drop an employee that is already there");
                return false;

            }

            return AddToQueue(employee);
        }

        return false;
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
        if (_currentDisplayedEmployee == null)
        {
            employee.gameObject.SetActive(true);
            Debug.Log("Set employee object active");
            _currentDisplayedEmployee = employee;
            _currentDisplayedEmployee.EmployeeInfoUI.gameObject.SetActive(true);
        }

        // Change employee's queue type
        employee.CurrentQueueType = QueueType.Reserves;
        employee.CurrentQueuePosition = _reservesQueue.Count - 1;

        // Update reserves UI
        UpdateUI();
        

        // Need to reset original position of the draggable object component
        if(employee.DraggableComponent != null)
        {
            employee.DraggableComponent.ResetOriginalPosition();
        }

        return true;
    }

    private void UpdateUI()
    {
        if(_reservesUI != null)
        {
            _reservesUI.UpdateView(_reservesQueue, _currentDisplayedEmployee);
        }
        SetQueueCountText();
    }

    private void SetQueueCountText()
    {
        _queueCountText.text = $"Reserves Size: {_reservesQueue.Count}";
    }

    public bool RemoveEmployee(Employee employee)
    {
        if (employee == null) return false;

        bool successfulRemoval = _reservesQueue.RemoveFromQueue(employee);
        if (!successfulRemoval)
        {
            Debug.Log("Failed to remove employee from reserves");
            return false;
        }

        // Update reserves UI
        UpdateUI();

        if(_currentDisplayedEmployee == employee)
        {
            _currentDisplayedEmployee = _reservesQueue.GetNextInQueue();
            if(_currentDisplayedEmployee != null)
            {
                _currentDisplayedEmployee.gameObject.SetActive(true);
                _currentDisplayedEmployee.EmployeeInfoUI.Show();
            }
        }

        return false;
    }
}
