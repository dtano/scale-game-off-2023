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

    private Employee _passenger;
    private int _passengerIndex;

    public UnityAction<int> OnIconClickEvent;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(Employee passenger, int index)
    {
        if (passenger == null) return;
        _passenger = passenger;
        _passengerIndex = index;

        _weightText.text = $"{passenger.Weight}kg";
        _destinationFloorText.text = $"{passenger.DestinationFloor}f";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnIconClickEvent != null) OnIconClickEvent.Invoke(_passengerIndex);
    }
}
