using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyType
{
    Tiny,
    Small,
    Medium,
    Large,
    Massive
}

public class Employee : MonoBehaviour
{
    [SerializeField] private Guid _id;
    [SerializeField] private int _weight;
    [SerializeField] private int _destinationFloor;
    [SerializeField] private float _satisfactionLevel = 100f;
    [SerializeField] private BodyType _bodyType;

    public Guid Id => _id;
    public int Weight => _weight;
    public int DestinationFloor => _destinationFloor;
    public float SatisfactionLevel => _satisfactionLevel;
    public BodyType BodyType => _bodyType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Sprite might also need to be added here
    public void SetEmployeeData(Guid id, BodyType bodyType, int weight, int destinationFloor, Sprite sprite, float satisfactionLevel = 100f)
    {
        _id = id;
        _bodyType = bodyType;
        _weight = weight;
        _destinationFloor = destinationFloor;
        _satisfactionLevel = satisfactionLevel;
    }

    public override bool Equals(object obj)
    {
        var other = obj as Employee;

        if (other == null)
        {
            return false;
        }

        return _id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}
