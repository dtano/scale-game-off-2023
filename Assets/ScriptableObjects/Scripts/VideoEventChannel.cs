using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Video Event Channel", menuName = "Scriptable Objects/Event Channels/Video")]
public class VideoEventChannel : ScriptableObject
{
    public UnityAction<VideoClip> OnRequestVideoEvent;

    public void RaiseVideoRequest(VideoClip clip)
    {
        if(OnRequestVideoEvent != null) OnRequestVideoEvent.Invoke(clip);
    }
}
