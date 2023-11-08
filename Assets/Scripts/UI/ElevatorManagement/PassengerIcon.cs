using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassengerIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _weightText;
    [SerializeField] private TextMeshProUGUI _destinationFloorText;

    private Employee _passenger;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(Employee passenger)
    {
        if (passenger == null) return;
        _passenger = passenger;

        _weightText.text = $"{passenger.Weight}kg";
        _destinationFloorText.text = $"{passenger.DestinationFloor}f";
    }
}
