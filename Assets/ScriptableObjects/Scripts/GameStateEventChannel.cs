using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Game State Event Channel", menuName = "Scriptable Objects/Event Channels/Game State")]
// A tad bit worried about whether or not these listeners will be 
public class GameStateEventChannel : ScriptableObject
{
    public UnityAction<bool> OnTabletStateChangeEvent;
    public UnityAction OnTimeLimitReachedEvent;
    public UnityAction OnAllEmployeesServedEvent;
    public UnityAction<List<Employee>, int> OnReleaseEmployeesInFloorEvent;
    public UnityAction OnSceneTransitionOverEvent;
    public UnityAction OnShowTutorialEvent;
    public UnityAction OnEndTutorialEvent;
    public UnityAction OnRequestNextLevelEvent;
    public UnityAction OnRequestRetryLevelEvent;
    public UnityAction OnRequestExitGameEvent;

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

    public void OnSceneTransitionOver()
    {
        if(OnSceneTransitionOverEvent != null) OnSceneTransitionOverEvent.Invoke();
    }

    public void OnShowTutorial()
    {
        if (OnShowTutorialEvent != null) OnShowTutorialEvent.Invoke();
    }

    public void OnEndTutorial()
    {
        if (OnEndTutorialEvent != null) OnEndTutorialEvent.Invoke();
    }

    public void OnRequestNextLevel()
    {
        if (OnRequestNextLevelEvent != null) OnRequestNextLevelEvent.Invoke();
    }

    public void OnRequestRetryLevel()
    {
        if(OnRequestRetryLevelEvent != null) OnRequestRetryLevelEvent.Invoke();
    }

    public void OnRequestExitGame()
    {
        if (OnRequestExitGameEvent != null) OnRequestExitGameEvent.Invoke();
    }
}
