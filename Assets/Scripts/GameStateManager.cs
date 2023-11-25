using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameStateEventChannel _eventChannel;
    [SerializeField] private GameFinishedUI _gameFinishedUI;
    public static GameStateManager Instance { get; private set; }

    private bool _isTabletOn = false;
    private bool _isGameOver = false;
    private bool _isTimeLimitReached = false;

    public bool IsTabletOn { get => _isTabletOn; set => _isTabletOn = value; }
    public bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }
    public bool IsTimeLimitReached { get => _isTimeLimitReached; set => _isTimeLimitReached = value; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            if(_eventChannel != null)
            {
                _eventChannel.OnTabletStateChangeEvent += SetTabletStatus;
                _eventChannel.OnTimeLimitReachedEvent += SetTimeLimitReachedState;
                _eventChannel.OnAllEmployeesServedEvent += SetGameWonState;
            }
        }
    }

    public void SetTabletStatus(bool isOn)
    {
        _isTabletOn = isOn;
    }

    public void SetTimeLimitReachedState()
    {
        SetGameIsOver();
        _isTimeLimitReached = true;
    }

    public void SetGameWonState()
    {
        SetGameIsOver();

        //if(_gameFinishedUI)
    }

    public void SetGameIsOver()
    {
        _isGameOver = true;
    }
}
