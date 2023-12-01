using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Details Object", menuName = "Scriptable Objects/Game Details")]
public class GameDetailsSO : ScriptableObject
{
    [SerializeField] private bool _showTutorialScreen = true;
    [SerializeField] private bool _hasCompletedEntireGame = false;
    
    public bool ShowTutorialScreen { get => _showTutorialScreen; set => _showTutorialScreen = value; }
    public bool HasCompletedEntireGame { get => _hasCompletedEntireGame; set => _hasCompletedEntireGame = value; }
}
