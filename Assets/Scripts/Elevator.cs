using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down
}

public class Elevator : MonoBehaviour
{
    [SerializeField] private HashSet<Employee> _passengers = new HashSet<Employee>();
    [SerializeField] private Direction _currentDirection = Direction.Up;
    
    private Dictionary<int, List<Employee>> _destinationMap = new Dictionary<int, List<Employee>>();
    private bool _shouldMove = false;

    public HashSet<Employee> Passengers => _passengers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateMovement()
    {
        _shouldMove = true;
    }

    // Before calling this, the employee must be removed from whatever queue they were in
    public void AddToElevator(Employee employee)
    {
        bool success = _passengers.Add(employee);
        if (!success)
        {
            Debug.Log("Fail to add employee to elevator");
            return;
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
    }
}
