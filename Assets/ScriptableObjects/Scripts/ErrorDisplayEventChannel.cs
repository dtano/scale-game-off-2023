using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Error Display Event Channel", menuName = "Scriptable Objects/Event Channels/Error Display")]
public class ErrorDisplayEventChannel : ScriptableObject
{
    public delegate void OnRequestErrorDelegate(ErrorDTO error);
    public event OnRequestErrorDelegate OnRequestErrorEvent;

    public void RequestError(ErrorDTO error)
    {
        if(OnRequestErrorEvent != null)
        {
            OnRequestErrorEvent.Invoke(error);
        }
    }
}
