using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : UIElement
{
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;

    private bool _isGamePaused = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClickResumeButton()
    {
        if (_gameStateEventChannel != null) _gameStateEventChannel.OnRequestResumeGame();
    }

    public void OnClickRetryButton()
    {
        if (_gameStateEventChannel != null) _gameStateEventChannel.OnRequestRetryLevel();
    }

    public void OnClickExitButton()
    {
        if (_gameStateEventChannel != null) _gameStateEventChannel.OnRequestExitGame();
    }
}
