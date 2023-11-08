using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElevatorPassengerList : UIElement
{
    [SerializeField] private GameObject _passengerIconPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPassengerInformation(Elevator elevator)
    {
        Dictionary<int, List<Employee>> destinationMap = elevator.DestinationMap;

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
                passengerIcon.SetData(employee);
                transform.GetChild(objectIndex).gameObject.SetActive(true);
                objectIndex++;
            }
        }
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

        //int i = 0;
        //while(i < amount)
        //{

        //    i++;
        //}
    }
}
