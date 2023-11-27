using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Cue", menuName = "Scriptable Objects/Audio")]
public class AudioCueSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _pitch = 1;
    [SerializeField] private bool _isLooping;

    public string Name => _name;
    public AudioClip Clip => _clip;
    public float Pitch => _pitch;
    public bool IsLooping => _isLooping;    
}
