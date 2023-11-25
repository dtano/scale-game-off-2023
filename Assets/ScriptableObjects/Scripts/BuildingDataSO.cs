using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Type Data", menuName = "Scriptable Objects/Building")]
public class BuildingDataSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _numFloors;

    public string Name => _name;
    public int NumFloors => _numFloors;
}
