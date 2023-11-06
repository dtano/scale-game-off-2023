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
    private Vector3 _restingPosition;

    // Start is called before the first frame update
    void Start()
    {
        TurnOff(shouldSlide: false);
        _restingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TurnOn(bool shouldSlide = true)
    {
        _isOn = true;

        if (!shouldSlide)
        {
            ShowAllComponents();
        }
        else
        {
            LeanTween.moveY(gameObject, 360, 0.5f).setOnComplete(ShowAllComponents);
        }
    }

    private void ShowAllComponents()
    {
        _elevatorSelectComponent.Show();
        _elevatorInfoComponent.Show();
        _elevatorPassengerList.Show();
        _elevatorPassengerInteractionModal.Show();
    }

    private void TurnOff(bool shouldSlide = true)
    {
        _isOn = false;

        if (!shouldSlide)
        {
            HideAllComponents();
        }
        else
        {
            HideAllComponents();
            LeanTween.moveY(gameObject, -191, 0.5f);
        }
    }

    private void HideAllComponents()
    {
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
