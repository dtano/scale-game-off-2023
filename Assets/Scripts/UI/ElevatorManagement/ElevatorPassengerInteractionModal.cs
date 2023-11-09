using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElevatorPassengerInteractionModal : UIElement
{
    [SerializeField] private TextMeshProUGUI _weightText;
    [SerializeField] private TextMeshProUGUI _destinationFloorText;
    [SerializeField] private TextMeshProUGUI _employeeName;
    [SerializeField] private TextMeshProUGUI _weightAfterKickText;
    [SerializeField] private GameObject _emptySelectionObject;
    [SerializeField] private Button _actionButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPassengerData(Elevator elevator, Employee employee)
    {
        if(employee == null)
        {
            ShowEmptySelectionState();
            return;
        }


        HandleNonEmptySelectionState();
        _weightText.text = $"{employee.Weight}kg";
        _destinationFloorText.text = $"{employee.DestinationFloor}f";

        int elevatorWeightAfterEmployeeRemoval = elevator.CurrentCapacity - employee.Weight;
        _weightAfterKickText.text = $"{((elevatorWeightAfterEmployeeRemoval > 0) ? elevatorWeightAfterEmployeeRemoval : 0)}/{elevator.MaxCapacity}";
    }

    private void ShowEmptySelectionState()
    {
        _destinationFloorText.gameObject.SetActive(false);
        _weightText.gameObject.SetActive(false);
        _employeeName.gameObject.SetActive(false);
        _weightAfterKickText.transform.parent.gameObject.SetActive(false);
        _actionButton.gameObject.SetActive(false);

        _emptySelectionObject.gameObject.SetActive(true);
    }

    private void HandleNonEmptySelectionState()
    {
        _destinationFloorText.gameObject.SetActive(true);
        _weightText.gameObject.SetActive(true);
        _employeeName.gameObject.SetActive(true);
        _weightAfterKickText.transform.parent.gameObject.SetActive(true);
        _actionButton.gameObject.SetActive(true);

        _emptySelectionObject.gameObject.SetActive(false);
    }
}
