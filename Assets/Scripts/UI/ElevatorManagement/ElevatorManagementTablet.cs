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

    [SerializeField] private Image _tabletImage;
    [SerializeField] private Sprite _tabletOnSprite;
    [SerializeField] private Sprite _tabletOffSprite;

    [SerializeField] private GameStateEventChannel _gameStateEventChannel;
    [SerializeField] private TabletInteractionEventChannel _tabletInteractionEventChannel;
    [SerializeField] private AudioEventChannel _sfxAudioEventChannel;
    [SerializeField] private AudioCueSO _tabletSwipeSfx;

    private List<Elevator> _allElevators;
    private int _currentlySelectedElevatorIndex;
    private float _tabletSwipeOffset = 0.5f;
    private bool _isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        TurnOff(shouldSlide: false);

        _allElevators = _buildingController.Elevators;

        _elevatorPassengerList.OnSelectEmployeeEvent += OnSelectEmployee;
        _elevatorPassengerList.OnKickEmployeeEvent += OnKickEmployeeFromElevator;
        
        _gameStateEventChannel.OnTimeLimitReachedEvent += ForceTurnOff;
        _gameStateEventChannel.OnAllEmployeesServedEvent += ForceTurnOff;
    }

    private void TurnOn(bool shouldSlide = true)
    {
        _isOn = true;
        _tabletImage.sprite = _tabletOnSprite;

        if (!shouldSlide)
        {
            OnTabletStart();
        }
        else
        {
            float offset = Screen.height * _tabletSwipeOffset;
            LeanTween.moveY(gameObject, transform.position.y + offset, 0.2f).setOnComplete(OnTabletStart);

            if (_sfxAudioEventChannel != null) _sfxAudioEventChannel.RaiseEvent(_tabletSwipeSfx);
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
        
        _elevatorSelectComponent.SetInformation(currentElevator.Data, _currentlySelectedElevatorIndex, _allElevators.Count);
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

    private void ForceTurnOff()
    {
        TurnOff();
    }

    private void TurnOff(bool shouldSlide = true)
    {
        _isOn = false;
        _tabletImage.sprite = _tabletOffSprite;

        if (!shouldSlide)
        {
            HideAllComponents();
        }
        else
        {
            HideAllComponents();

            float offset = Screen.height * _tabletSwipeOffset;
            LeanTween.moveY(gameObject, transform.position.y - offset, 0.2f);
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
        if (GameStateManager.Instance.IsGameOver) return;
        if (_isOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    public bool OnKickEmployeeFromElevator(Employee employee)
    {
        Elevator currentSelectedElevator = _allElevators[_currentlySelectedElevatorIndex];

        bool isSuccessfullyKicked = false;
        if (_tabletInteractionEventChannel != null)
        {
            isSuccessfullyKicked = _tabletInteractionEventChannel.OnKickEmployeeFromElevator(currentSelectedElevator, employee);
        }

        // Need to update view now
        UpdateView();
        return isSuccessfullyKicked;
    }

    private void OnDestroy()
    {
        _gameStateEventChannel.OnTimeLimitReachedEvent -= ForceTurnOff;
        _gameStateEventChannel.OnAllEmployeesServedEvent -= ForceTurnOff;
    }
}
