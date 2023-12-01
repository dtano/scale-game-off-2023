using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionUI : MonoBehaviour
{
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;
    [SerializeField] private bool _shouldTriggerEvent = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTransitionOver()
    {
        if (!_shouldTriggerEvent) return;

        if (_gameStateEventChannel != null) _gameStateEventChannel.OnSceneTransitionOver();
    }
}
