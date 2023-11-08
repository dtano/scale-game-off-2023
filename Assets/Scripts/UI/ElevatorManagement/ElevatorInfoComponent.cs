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
        _capacityText.text = $"{elevator.CurrentCapacity}/{elevator.MaxCapacity}";
    }
}
