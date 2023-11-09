using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Game State Event Channel", menuName = "Scriptable Objects/Event Channels/Game State")]
public class GameStateEventChannel : ScriptableObject
{
    public UnityAction<bool> OnTabletStateChangeEvent;

    public void OnTabletStateChange(bool isOn)
    {
        if (OnTabletStateChangeEvent != null) OnTabletStateChangeEvent.Invoke(isOn);
    }
}
