using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weightText;
    [SerializeField] private TextMeshProUGUI _destinationFloorText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetInformation(int weight, int destinationFloor)
    {
        _weightText.text = $"{weight} kg";
        _destinationFloorText.text = destinationFloor.ToString();

        Show();
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
