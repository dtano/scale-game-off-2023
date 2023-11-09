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

    [SerializeField] private GameStateEventChannel _gameStateEventChannel;

    private List<Elevator> _allElevators;
    private int _currentlySelectedElevatorIndex;
    private bool _isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        TurnOff(shouldSlide: false);

        _allElevators = _buildingController.Elevators;

        _elevatorPassengerList.OnSelectEmployeeEvent += OnSelectEmployee;
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
            OnTabletStart();
        }
        else
        {
            LeanTween.moveY(gameObject, 360, 0.2f).setOnComplete(OnTabletStart);
        }

        if (_gameStateEventChannel != null) _gameStateEventChannel.OnTabletStateChange(_isOn);
    }

    public void OnClickNext()
    {
        if(_currentlySelectedElevatorIndex == _allElevators.Count - 1)
        {
            return;
        }

        _currentlySelectedElevatorIndex++;

        UpdateView();
    }

    private void UpdateView()
    {
        Elevator currentElevator = _allElevators[_currentlySelectedElevatorIndex];
        
        _elevatorSelectComponent.SetInformation(_currentlySelectedElevatorIndex, _allElevators.Count);
        _elevatorInfoComponent.SetInformation(currentElevator);
        _elevatorPassengerList.SetPassengerInformation(currentElevator);
        _elevatorPassengerInteractionModal.SetPassengerData(currentElevator, _elevatorPassengerList.CurrentSelectedEmployee);
    }

    public void OnClickBack()
    {
        if(_currentlySelectedElevatorIndex == 0)
        {
            return;
        }

        _currentlySelectedElevatorIndex--;
        UpdateView();
    }

    private void OnTabletStart()
    {
        UpdateView();

        ShowAllComponents();
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
            LeanTween.moveY(gameObject, -191, 0.2f);
        }

        if (_gameStateEventChannel != null) _gameStateEventChannel.OnTabletStateChange(_isOn);
    }

    private void HideAllComponents()
    {
        _elevatorSelectComponent.Hide();
        _elevatorInfoComponent.Hide();
        _elevatorPassengerList.Hide();
        _elevatorPassengerInteractionModal.Hide();
    }

    private void OnSelectEmployee(Employee employee)
    {
        _elevatorPassengerInteractionModal.SetPassengerData(_allElevators[_currentlySelectedElevatorIndex], employee);
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
