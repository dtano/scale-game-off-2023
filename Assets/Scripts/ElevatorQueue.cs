using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorQueue : MonoBehaviour
{
    [SerializeField] private List<Employee> _queue;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private bool _hasMaxCapacity = false;

    public List<Employee> Queue => _queue;
    public int Count => _queue.Count;
    public int MaxCapacity => _maxCapacity;
    void Awake()
    {
        _queue = new List<Employee>();
    }

    public bool AddToQueue(Employee employee)
    {
        if(_hasMaxCapacity && _queue.Count >= _maxCapacity)
        {
            return false;
        }

        employee.gameObject.SetActive(false);
        _queue.Add(employee);

        return true;
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

    public Employee GetByIndex(int index)
    {
        return _queue[index];
    }

    public int GetRemainingCount()
    {
        return _queue.Count;
    }
}
