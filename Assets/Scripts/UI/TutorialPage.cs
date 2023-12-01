using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class TutorialPage : UIElement
{
    [SerializeField] private VideoClip _videoClip;
    public UnityAction<VideoClip> OnRequestPlayClipEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Page Start!");    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Show()
    {
        if (_videoClip != null) OnRequestPlayClipEvent?.Invoke(_videoClip);
        base.Show();
    }
}
