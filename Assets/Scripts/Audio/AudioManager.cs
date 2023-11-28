using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is mainly for interaction audio
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEventChannel _sfxEventChannel;
    [SerializeField] private AudioCueSO _playerLostSfx;
    [SerializeField] private AudioCueSO _playerWonSfx;
    private AudioSource _sfxAudioSource;

    // Start is called before the first frame update
    void Awake()
    {
        _sfxEventChannel.OnAudioCueRequested += PlayAudioCue;
        _sfxEventChannel.OnPlayerLost += OnPlayerLost;
        _sfxEventChannel.OnPlayerWon += OnPlayerWon;
        _sfxAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayAudioCue(AudioCueSO audioCue)
    {
        if (audioCue == null) return;

        Debug.Log("PLAYING AUDIO CUE!! " + audioCue.Name);
        _sfxAudioSource.loop = audioCue.IsLooping;
        _sfxAudioSource.pitch = audioCue.Pitch;
        _sfxAudioSource.clip = audioCue.Clip;
        _sfxAudioSource.Play();
    }

    private void OnPlayerLost()
    {
        PlayAudioCue(_playerLostSfx);
    }

    private void OnPlayerWon()
    {
        PlayAudioCue(_playerWonSfx);
    }

    private void OnDestroy()
    {
        _sfxEventChannel.OnAudioCueRequested -= PlayAudioCue;
        _sfxEventChannel.OnPlayerWon -= OnPlayerWon;
        _sfxEventChannel.OnPlayerLost -= OnPlayerLost;
    }
}
