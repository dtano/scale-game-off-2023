using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] private GameStateEventChannel _eventChannel;
    [SerializeField] private TextMeshProUGUI _clockText;
    [SerializeField] private int _startHour = 8;
    [SerializeField] private int _startMinute = 0;
    [SerializeField] private int _timerDurationInMinutes;

    private bool _isRunning = true;
    private int _currentHour;
    private int _currentMinutes;
    private int _minutesElapsed;

    private float _currentTime;

    public bool IsRunning { get => _isRunning; set => _isRunning = value; }
    public float ElapsedTime => _currentTime;

    // Start is called before the first frame update
    void Awake()
    {
        _currentHour = _startHour;
        _currentMinutes = _startMinute;

        _eventChannel.OnAllEmployeesServedEvent += TurnOff;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            if(_minutesElapsed == _timerDurationInMinutes)
            {
                OnTimerFinished();
                return;
            }

            // Update time here
            _currentTime += Time.deltaTime / TimeConstants.SECONDS_PER_MINUTE;

            _minutesElapsed = Mathf.FloorToInt(_currentTime);
            _currentMinutes = Mathf.FloorToInt((_startMinute + _currentTime) % 60f);
            _currentHour = Mathf.FloorToInt(_startHour + ((_startMinute + _currentTime) / 60f));

            UpdateTimeText();
        }
    }

    private void UpdateTimeText()
    {
        _clockText.text = string.Format("{0:00}:{1:00}", _currentHour, _currentMinutes);
    }

    private void OnTimerFinished()
    {
        Debug.Log("Timer finished");
        
        // Trigger some events
        if(_eventChannel != null)
        {
            _eventChannel.OnTimeLimitReached();
        }

        TurnOff();
    }

    public void TurnOn()
    {
        _isRunning = true;
    }

    public void TurnOff()
    {
        _isRunning = false;

        // And store the time elapsed
        Debug.Log("Minutes elapsed: " + _minutesElapsed);

        float minutes = Mathf.FloorToInt(_currentTime / 60);
        float seconds = Mathf.FloorToInt(_currentTime % 60);

        string msFormat = string.Format("{0:00}:{1:00}", minutes, seconds);
        Debug.Log($"Minutes and seconds elapsed: {msFormat}");
    }
}
