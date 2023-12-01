using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionUI : MonoBehaviour
{
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;
    [SerializeField] private bool _shouldTriggerEvent = true;

    public void OnTransitionOver()
    {
        if (!_shouldTriggerEvent) return;

        if (_gameStateEventChannel != null) _gameStateEventChannel.OnSceneTransitionOver();
    }
}
