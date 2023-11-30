using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElevatorInfoComponent : UIElement
{
    [SerializeField] private TextMeshProUGUI _capacityText;
    [SerializeField] private TextMeshProUGUI _speedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInformation(Elevator elevator)
    {
        _capacityText.text = $"Capacity: {elevator.CurrentCapacity}/{elevator.MaxCapacity}";

        if(elevator.Speed % TimeConstants.SECONDS_PER_MINUTE == 0)
        {
            _speedText.text = $"Speed: {elevator.Speed / TimeConstants.SECONDS_PER_MINUTE} minutes";
        }
        else
        {
            _speedText.text = $"Speed: {(elevator.Speed / TimeConstants.SECONDS_PER_MINUTE) * 60} seconds";
        }
    }
}
