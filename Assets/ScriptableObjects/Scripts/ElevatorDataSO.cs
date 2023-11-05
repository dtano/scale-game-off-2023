using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Elevator", menuName = "Scriptable Objects/Elevator")]
public class ElevatorDataSO : ScriptableObject
{
    [SerializeField] private string _model;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private Sprite _sprite;
    // Audio clip related stuff here

    public string Model => _model;
    public int MaxCapacity => _maxCapacity;
    public Sprite Sprite => _sprite;
}
