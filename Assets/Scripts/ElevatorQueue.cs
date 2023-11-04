using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorQueue : MonoBehaviour
{
    private List<Employee> _queue;

    void Awake()
    {
        _queue = new List<Employee>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToQueue(Employee employee)
    {
        _queue.Add(employee);
    }

    public Employee GetNextInQueue()
    {
        if (_queue.Count == 0) return null;
        return _queue[0];
    }

    //public Employee RemoveFromQueue(Employee employee)
    //{
    //    Employee employeeToRemove = _queue.Find(_employee => _employee == employee);
    //}
}
