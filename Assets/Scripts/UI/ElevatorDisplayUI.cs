using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElevatorDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _capacityText;
    [SerializeField] private Button _goButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInformation(int currentCapacity, int maxCapacity)
    {
        _capacityText.text = $"{currentCapacity}/{maxCapacity}";
    }

    public void SetButtonAvailability(bool isDisabled)
    {
        if (isDisabled) _goButton.enabled = false;

        _goButton.enabled = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
