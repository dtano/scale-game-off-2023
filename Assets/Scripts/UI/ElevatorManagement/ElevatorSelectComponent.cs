using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElevatorSelectComponent : UIElement
{
    [SerializeField] private TextMeshProUGUI _elevatorNameText;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _backButton;

    private int _currentIndex;
    private int _totalElevators;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetElevatorName(string name)
    {
        _elevatorNameText.text = name;
    }

    public void SetInformation(int currentIndex, int totalElevators)
    {
        _currentIndex = currentIndex;
        _totalElevators = totalElevators;

        _elevatorNameText.text = $"Elevator {_currentIndex + 1}";

        if(_currentIndex == 0)
        {
            _backButton.gameObject.SetActive(false);
        }
        else
        {
            _backButton.gameObject.SetActive(true);
        }

        if(_currentIndex == totalElevators - 1 || _currentIndex == totalElevators)
        {
            _nextButton.gameObject.SetActive(false);
        }
        else
        {
            _nextButton.gameObject.SetActive(true);
        }
    }
}
