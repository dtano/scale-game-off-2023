using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElevatorInformationUI : MonoBehaviour
{
    [SerializeField] private ElevatorDataSO _elevatorData;
    [SerializeField] private TextMeshProUGUI _elevatorName;
    [SerializeField] private TextMeshProUGUI _elevatorMaxCapacity;
    [SerializeField] private TextMeshProUGUI _elevatorSpeed;
    [SerializeField] private Image _elevatorImage;

    // Start is called before the first frame update
    void Awake()
    {
        if(_elevatorData != null)
        {
            SetInformation();
        }
    }

    private void SetInformation()
    {
        _elevatorImage.sprite = _elevatorData.Sprite;
        _elevatorName.text = _elevatorData.Model;
        _elevatorMaxCapacity.text = $"{_elevatorData.MaxCapacity} kg";

        if (_elevatorData.SpeedInSeconds % TimeConstants.SECONDS_PER_MINUTE == 0)
        {
            _elevatorSpeed.text = $"{_elevatorData.SpeedInSeconds / TimeConstants.SECONDS_PER_MINUTE} minute(s)";
        }
        else
        {
            _elevatorSpeed.text = $"{(_elevatorData.SpeedInSeconds / TimeConstants.SECONDS_PER_MINUTE) * 60} seconds";
        }
    }
}
