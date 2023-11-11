using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ElevatorPassengerList : UIElement
{
    [SerializeField] private GameObject _passengerIconPrefab;

    private List<PassengerIcon> _passengerIcons = new List<PassengerIcon>();
    private List<Employee> _employees = new List<Employee>();
    private int _currentSelectedPassengerIndex;

    public UnityAction<Employee> OnSelectEmployeeEvent;
    public UnityAction<Employee> OnKickEmployeeEvent;

    public Employee CurrentSelectedEmployee => GetCurrentSelectedEmployee();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickPassengerIcon(int passengerIndex)
    {
        Employee selectedEmployee = _employees[passengerIndex];
        _currentSelectedPassengerIndex = passengerIndex;
        if (OnSelectEmployeeEvent != null) OnSelectEmployeeEvent.Invoke(selectedEmployee);
    }

    public void OnKickEmployeeFromElevator()
    {
        Employee employeeToKick = _employees[_currentSelectedPassengerIndex];

        _employees.RemoveAt(_currentSelectedPassengerIndex);

        OnKickEmployeeEvent?.Invoke(employeeToKick);
    }

    public void SetPassengerInformation(Elevator elevator)
    {
        Dictionary<int, List<Employee>> destinationMap = elevator.DestinationMap;
        _employees.Clear();

        List<int> destinationKeys = destinationMap.Keys.ToList();
        destinationKeys.Sort();

        // Get count of how many passenger icons we have rn
        int totalCount = 0;
        foreach(int key in destinationKeys)
        {
            totalCount += destinationMap[key].Count;
        }

        // Determine what to do with the passenger objects
        int childCount = GetNumberOfActiveChildren();
        if (totalCount < transform.childCount)
        {
            HidePassengerIcons(transform.childCount - totalCount);
        }
        else if(totalCount > transform.childCount)
        {
            InstantiatePassengerIcons(totalCount - transform.childCount);
        }

        // Set the passenger info
        int objectIndex = 0;
        foreach(int key in destinationKeys)
        {
            foreach(Employee employee in destinationMap[key])
            {
                PassengerIcon passengerIcon = transform.GetChild(objectIndex).GetComponent<PassengerIcon>();
                passengerIcon.SetData(employee, objectIndex);
                if (passengerIcon.OnIconClickEvent == null) passengerIcon.OnIconClickEvent += OnClickPassengerIcon;
                transform.GetChild(objectIndex).gameObject.SetActive(true);

                _passengerIcons.Add(passengerIcon);
                _employees.Add(employee);
                objectIndex++;
            }
        }

        _currentSelectedPassengerIndex = 0;
    }

    private int GetNumberOfActiveChildren()
    {
        int count = 0;
        foreach(Transform child in transform)
        {
            if(child.gameObject.activeSelf) count++;
        }

        return count;
    }

    private void InstantiatePassengerIcons(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(_passengerIconPrefab, transform);
        }
    }

    private void HidePassengerIcons(int amount)
    {
        for(int i = transform.childCount - 1; i >= (transform.childCount - amount); i--)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private Employee GetCurrentSelectedEmployee()
    {
        if (_employees == null || _employees.Count == 0 || _currentSelectedPassengerIndex < 0) return null;

        return _employees[_currentSelectedPassengerIndex];
    }
}
