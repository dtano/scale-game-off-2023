using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : UIElement
{
    [SerializeField] private TutorialScreen _tutorialScreen;
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;
    [SerializeField] private PauseMenu _pauseMenu;
    private bool _isGamePaused = false;

    // Start is called before the first frame update
    void Awake()
    {
        if(_tutorialScreen != null) _tutorialScreen.Hide();
        if(_pauseMenu != null) _pauseMenu.Hide();

        if(_gameStateEventChannel != null)
        {
            _gameStateEventChannel.OnRequestResumeGameEvent += ResumeGame;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandlePauseMenu();
    }

    private void HandlePauseMenu()
    {
        if (_pauseMenu == null) return;
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePaused)
            {
                ResumeGame();
                return;
            }

            PauseGame();
        }
    }

    private void ResumeGame()
    {
        _isGamePaused = false;
        _pauseMenu.Hide();
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        _isGamePaused = true;
        _pauseMenu.Show();
        Time.timeScale = 0;
    }

    public void ShowTutorial()
    {
        _tutorialScreen.Show();
    }

    public void HideTutorial()
    {
        _tutorialScreen.Hide();
    }

    private void OnDestroy()
    {
        if (_gameStateEventChannel != null)
        {
            _gameStateEventChannel.OnRequestResumeGameEvent -= ResumeGame;
        }
    }
}
