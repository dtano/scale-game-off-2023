using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionUI : MonoBehaviour
{
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;

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
        if (_gameStateEventChannel != null) _gameStateEventChannel.OnSceneTransitionOver();
    }
}
