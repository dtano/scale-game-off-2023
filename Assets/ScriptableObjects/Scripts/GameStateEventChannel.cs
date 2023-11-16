using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Game State Event Channel", menuName = "Scriptable Objects/Event Channels/Game State")]
public class GameStateEventChannel : ScriptableObject
{
    public UnityAction<bool> OnTabletStateChangeEvent;
    public UnityAction OnTimeLimitReachedEvent;
    public UnityAction OnAllEmployeesServedEvent;
    public UnityAction<List<Employee>, int> OnReleaseEmployeesInFloorEvent;

    public void OnTabletStateChange(bool isOn)
    {
        if (OnTabletStateChangeEvent != null) OnTabletStateChangeEvent.Invoke(isOn);
    }

    public void OnTimeLimitReached()
    {
        Debug.Log("TIME LIMIT REACHED"); // Need to check whether the player finished right on the dot or not
        if(OnTimeLimitReachedEvent != null) OnTimeLimitReachedEvent.Invoke();
    }

    public void OnAllEmployeesServed()
    {
        if(OnAllEmployeesServedEvent != null) OnAllEmployeesServedEvent.Invoke();
    }

    public void OnReleaseEmployeesInFloor(List<Employee> employees, int floorNum)
    {
        if(OnReleaseEmployeesInFloorEvent != null) OnReleaseEmployeesInFloorEvent.Invoke(employees, floorNum);
    }
}
