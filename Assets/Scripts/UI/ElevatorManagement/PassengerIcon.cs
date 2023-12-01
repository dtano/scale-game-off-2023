using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class PassengerIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _weightText;
    [SerializeField] private TextMeshProUGUI _destinationFloorText;
    [SerializeField] private Image _selectedIndicator;

    private int _passengerIndex;

    public UnityAction<int> OnIconClickEvent;
  
    public void SetData(Employee passenger, int index)
    {
        if (passenger == null) return;
        _passengerIndex = index;

        if(_weightText != null) _weightText.text = $"{passenger.Weight}kg";
        if(_destinationFloorText != null) _destinationFloorText.text = $"{passenger.DestinationFloor}f";

        if(_icon != null && passenger.BodyTypeData != null)
        {
            _icon.sprite = passenger.BodyTypeData.IconSprite;
        }
    }

    public void SetSelectedIndicator(bool isSelected)
    {
        if (_selectedIndicator == null) return;
        _selectedIndicator.gameObject.SetActive(isSelected);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnIconClickEvent != null) OnIconClickEvent.Invoke(_passengerIndex);
    }
}
