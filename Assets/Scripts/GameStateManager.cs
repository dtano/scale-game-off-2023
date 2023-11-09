using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameStateEventChannel _eventChannel;
    public static GameStateManager Instance { get; private set; }

    private bool _isTabletOn = false;

    public bool IsTabletOn { get => _isTabletOn; set => _isTabletOn = value; }

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
            }
        }
    }

    public void SetTabletStatus(bool isOn)
    {
        _isTabletOn = isOn;
    }
}
