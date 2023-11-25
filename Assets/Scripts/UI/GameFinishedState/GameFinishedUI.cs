using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameFinishedUI : UIElement
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private GameWonUI _gameWonScreen;
    [SerializeField] private GameOverUI _gameOverScreen;

    private Animator _animator;
    
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string BuildPercentageText(int numEmployeesServed, int totalEmployees)
    {
        int percentage = Mathf.FloorToInt((numEmployeesServed / totalEmployees) * 100);
        return $"{percentage}% ({numEmployeesServed}/{totalEmployees})";
    }

    private string BuildTimeTakenText(float totalTimeTaken)
    {
        Debug.Log("Total time taken: " + totalTimeTaken);
        float minutes = Mathf.FloorToInt(totalTimeTaken / 60);
        float seconds = Mathf.FloorToInt(totalTimeTaken % 60);

        string msFormat = string.Format("{0:00}:{1:00}", minutes, seconds);

        return msFormat;
    }

    public void ShowGameWon(int numEmployeesServed, int totalEmployees, float totalTimeTaken)
    {
        _gameOverScreen.Hide();
        _titleText.text = "Level Complete";
        
        _gameWonScreen.Show();
        // Update screen view here
        _gameWonScreen.SetInformation(BuildPercentageText(numEmployeesServed, totalEmployees), BuildTimeTakenText(totalTimeTaken));
    }

    public void ShowGameOver(int numEmployeesServed, int totalEmployees)
    {
        _gameWonScreen.Hide();

        _titleText.text = "Time's Up";
        _gameOverScreen.Show();
        _gameOverScreen.SetInformation(BuildPercentageText(numEmployeesServed, totalEmployees));
    }
}
