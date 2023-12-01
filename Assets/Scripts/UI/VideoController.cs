using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private VideoEventChannel _channel;
    // Start is called before the first frame update
    
    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        if (_channel != null) _channel.OnRequestVideoEvent += PlayClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClip(VideoClip clip)
    {
        _videoPlayer.clip = clip;
        _videoPlayer.Play();
    }

    private void OnDestroy()
    {
        if (_channel != null) _channel.OnRequestVideoEvent -= PlayClip;
    }
}
