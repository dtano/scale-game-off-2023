using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Audio Event Channel", menuName = "Scriptable Objects/Event Channels/Audio")]
public class AudioEventChannel : ScriptableObject
{
    public UnityAction<AudioCueSO> OnAudioCueRequested;


    public void RaiseEvent(AudioCueSO audioCue)
    {
        if(OnAudioCueRequested != null)
        {
            OnAudioCueRequested.Invoke(audioCue);
        }
        else
        {
            Debug.LogWarning("An audio cue was requested, but nobody picked it up");
        }
    }
}
