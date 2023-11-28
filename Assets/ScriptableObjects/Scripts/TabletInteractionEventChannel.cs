using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Tablet Interaction Event Channel", menuName = "Scriptable Objects/Event Channels/Tablet")]
public class TabletInteractionEventChannel : ScriptableObject
{
    public delegate bool OnKickEmployeeFromElevatorDelegate(Elevator elevator, Employee employee);
    public event OnKickEmployeeFromElevatorDelegate OnKickEmployeeFromElevatorEvent;

    public bool OnKickEmployeeFromElevator(Elevator elevator, Employee employee)
    {
        if (OnKickEmployeeFromElevatorEvent != null)
        {
            return OnKickEmployeeFromElevatorEvent.Invoke(elevator, employee);
        }

        return false;
    }
}
