using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Cue", menuName = "Scriptable Objects/Audio")]
public class AudioCueSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;

    public string Name => _name;
    public AudioClip Clip => _clip;
}
