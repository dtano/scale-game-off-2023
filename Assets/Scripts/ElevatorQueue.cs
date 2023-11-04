using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorQueue : MonoBehaviour
{
    [SerializeField] private List<Employee> _queue;

    void Awake()
    {
        _queue = new List<Employee>();
    }

    public void AddToQueue(Employee employee)
    {
        employee.gameObject.SetActive(false);
        _queue.Add(employee);
    }

    public void AddToTop(Employee employee)
    {
        _queue.Insert(0, employee);
    }

    public Employee GetNextInQueue()
    {
        if (_queue.Count == 0) return null;

        _queue[0].gameObject.SetActive(true);
        return _queue[0];
    }

    public Employee GetSpecificEmployee(Employee employee)
    {
        if (_queue.Count == 0) return null;

        return _queue.Find(x => x.Equals(employee));
    }

    public bool RemoveFromQueue(Employee employee)
    {
        if (_queue.Count == 0) return false;

        return _queue.Remove(employee);
    }

    public int GetRemainingCount()
    {
        return _queue.Count;
    }
}
