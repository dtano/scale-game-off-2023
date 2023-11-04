using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Need to pass in some sort of map that contains the chances of each body type appearing
public class EmployeeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _employeePrefab;
    [SerializeField] private Transform _employeeObjParent;
    [SerializeField] private int _numEmployeesToSpawn;
    [SerializeField] private List<BodyTypeDataSO> _bodyTypeDataCollection;
    [SerializeField] private ElevatorQueue _elevatorQueue;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEmployees()
    {
        for(int i = 0; i < _numEmployeesToSpawn; i++)
        {
            CreateEmployee(false);
        }
    }

    public void CreateEmployee(bool isContinuousSpawn = true)
    {
        // Determine a random BodyType
        int randomBodyTypeIndex = UnityEngine.Random.Range(0, _bodyTypeDataCollection.Count);
        BodyTypeDataSO bodyTypeData = _bodyTypeDataCollection[randomBodyTypeIndex];

        // Determine random weight
        int weight = RandomUtils.GetRandomValueFromRange(bodyTypeData.MinWeight, bodyTypeData.MaxWeight);

        // Determine a random Destination Floor
        int destinationFloor = 12;

        // Set the results in the instantiated employee object
        GameObject employeeObject = Instantiate(_employeePrefab, _employeeObjParent.transform.position, Quaternion.identity, _employeeObjParent);
        if(employeeObject.TryGetComponent(out Employee employee))
        {
            Guid employeeId = Guid.NewGuid();
            employee.SetEmployeeData(employeeId, bodyTypeData.BodyType, weight, destinationFloor, bodyTypeData.Sprite);
            if(_elevatorQueue != null) _elevatorQueue.AddToQueue(employee);
        }

        if (isContinuousSpawn)
        {
            _numEmployeesToSpawn--;
        }
    }
}
