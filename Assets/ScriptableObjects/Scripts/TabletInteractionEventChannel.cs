using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Tablet Interaction Event Channel", menuName = "Scriptable Objects/Event Channels/Tablet")]
public class TabletInteractionEventChannel : ScriptableObject
{
    public UnityAction<Elevator, Employee> OnKickEmployeeFromElevatorEvent;

    public void OnKickEmployeeFromElevator(Elevator elevator, Employee employee)
    {
        if(OnKickEmployeeFromElevatorEvent != null) OnKickEmployeeFromElevatorEvent.Invoke(elevator, employee);
    }
}
