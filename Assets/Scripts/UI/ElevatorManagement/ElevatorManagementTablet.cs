using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorManagementTablet : UIElement
{
    [SerializeField] private BuildingController _buildingController;
    [SerializeField] private ElevatorSelectComponent _elevatorSelectComponent;
    [SerializeField] private ElevatorInfoComponent _elevatorInfoComponent;
    [SerializeField] private ElevatorPassengerList _elevatorPassengerList;
    [SerializeField] private ElevatorPassengerInteractionModal _elevatorPassengerInteractionModal;

    private bool _isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TurnOn()
    {
        _isOn = true;

        _elevatorSelectComponent.Show();
        _elevatorInfoComponent.Show();
        _elevatorPassengerList.Show();
        _elevatorPassengerInteractionModal.Show();
    }

    private void TurnOff()
    {
        _isOn = false;

        _elevatorSelectComponent.Hide();
        _elevatorInfoComponent.Hide();
        _elevatorPassengerList.Hide();
        _elevatorPassengerInteractionModal.Hide();
    }

    public void OnPowerButtonClick()
    {
        if (_isOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }
}
