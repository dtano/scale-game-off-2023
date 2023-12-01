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

    public void SetElevatorName(string name)
    {
        _elevatorNameText.text = name;
    }

    public void SetInformation(ElevatorDataSO elevatorData, int currentIndex, int totalElevators)
    {
        _currentIndex = currentIndex;

        _elevatorNameText.text = $"{elevatorData.Model}";

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
