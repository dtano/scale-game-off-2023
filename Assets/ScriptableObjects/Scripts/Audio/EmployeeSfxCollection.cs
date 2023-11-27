using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Employee SFX Collection", menuName = "Scriptable Objects/Employee/SFX")]
public class EmployeeSfxCollection : ScriptableObject
{
    [SerializeField] private AudioCueSO _onDragSfx;

    public AudioCueSO OnDragSfx => _onDragSfx;
}
