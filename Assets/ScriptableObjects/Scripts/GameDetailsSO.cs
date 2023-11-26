using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Details Object", menuName = "Scriptable Objects/Game Details")]
public class GameDetailsSO : ScriptableObject
{
    [SerializeField] private bool _showTutorialScreen = true;
    
    public bool ShowTutorialScreen { get => _showTutorialScreen; set => _showTutorialScreen = value; }
}
