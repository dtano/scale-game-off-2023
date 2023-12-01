using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class TutorialPage : UIElement
{
    [SerializeField] private VideoClip _videoClip;
    public UnityAction<VideoClip> OnRequestPlayClipEvent;

    public override void Show()
    {
        if (_videoClip != null) OnRequestPlayClipEvent?.Invoke(_videoClip);
        base.Show();
    }
}
