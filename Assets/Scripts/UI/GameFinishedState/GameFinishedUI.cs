using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameFinishedUI : UIElement
{
    [SerializeField] private GameStateEventChannel _gameStateEventChannel;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private GameWonUI _gameWonScreen;
    [SerializeField] private GameOverUI _gameOverScreen;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _gameWonSprite;
    [SerializeField] private Sprite _gameOverSprite;

    private Animator _animator;
    
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnClickContinue()
    {
        if(_gameStateEventChannel != null) _gameStateEventChannel.OnRequestNextLevel();
    }

    public void OnClickRetry()
    {
        if(_gameStateEventChannel != null) _gameStateEventChannel.OnRequestRetryLevel();
    }

    public void OnClickExit()
    {
        if (_gameStateEventChannel != null) _gameStateEventChannel.OnRequestExitGame();
    }

    private string BuildPercentageText(int numEmployeesServed, int totalEmployees)
    {
        int percentage = Mathf.FloorToInt((numEmployeesServed / (float)totalEmployees) * 100);
        return $"{percentage}% ({numEmployeesServed}/{totalEmployees})";
    }

    private string BuildTimeTakenText(float totalTimeTaken)
    {
        Debug.Log("Total time taken: " + totalTimeTaken);

        float minutes = Mathf.FloorToInt(totalTimeTaken % 60);
        float seconds = Mathf.FloorToInt((totalTimeTaken - minutes) * 60);

        string msFormat = string.Format("{0:00}:{1:00}", minutes, seconds);

        return msFormat;
    }

    public void ShowGameWon(int numEmployeesServed, int totalEmployees, float totalTimeTaken)
    {
        _gameOverScreen.Hide();
        _titleText.text = "Level Complete";
        
        _gameWonScreen.Show();
        // Update screen view here
        _icon.sprite = _gameWonSprite;
        _gameWonScreen.SetInformation(BuildPercentageText(numEmployeesServed, totalEmployees), BuildTimeTakenText(totalTimeTaken));
    }

    public void ShowGameOver(int numEmployeesServed, int totalEmployees)
    {
        _gameWonScreen.Hide();

        _titleText.text = "Time's Up";
        _icon.sprite = _gameOverSprite;
        _gameOverScreen.Show();
        _gameOverScreen.SetInformation(BuildPercentageText(numEmployeesServed, totalEmployees));
    }
}
