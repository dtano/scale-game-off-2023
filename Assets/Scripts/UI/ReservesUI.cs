using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReservesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountInQueueText;
    [SerializeField] private GameObject _employeeIconPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAmount(int queueLength)
    {
        _amountInQueueText.text = $"Reserves Size: {queueLength}";
    }
}
