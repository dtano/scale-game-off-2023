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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToQueue(Employee employee)
    {
        employee.gameObject.SetActive(false);
        _queue.Add(employee);
    }

    public Employee GetNextInQueue()
    {
        if (_queue.Count == 0) return null;

        _queue[0].gameObject.SetActive(true);
        return _queue[0];
    }
}
