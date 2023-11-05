using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is mainly for interaction audio
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEventChannel _sfxEventChannel;

    // Start is called before the first frame update
    void Awake()
    {
        _sfxEventChannel.OnAudioCueRequested += PlayAudioCue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayAudioCue(AudioCueSO audioCue)
    {

    }
}
