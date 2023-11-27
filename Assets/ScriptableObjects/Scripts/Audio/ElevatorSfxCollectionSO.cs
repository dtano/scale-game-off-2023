using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Elevator SFX Collection", menuName = "Scriptable Objects/Elevator/SFX")]
public class ElevatorSfxCollectionSO : ScriptableObject
{
    [SerializeField] private AudioCueSO _openSfx;
    [SerializeField] private AudioCueSO _closeSfx;
    [SerializeField] private AudioCueSO _movementSfx;
    [SerializeField] private AudioCueSO _reachedDestinationSfx;
    [SerializeField] private AudioCueSO _pressCloseButtonSfx;

    public AudioCueSO OpenSfx => _openSfx;
    public AudioCueSO CloseSfx => _closeSfx;
    public AudioCueSO MovementSfx => _movementSfx;
    public AudioCueSO ReachedDestinationSfx => _reachedDestinationSfx;
    public AudioCueSO PressCloseButtonSfx => _pressCloseButtonSfx;
}
