using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _floorsValueText;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInformation(BuildingDataSO buildingData)
    {
        _buildingNameText.text = buildingData.Name;
        _floorsValueText.text = (buildingData.NumFloors > 0 ? buildingData.NumFloors - 1 : 0).ToString();
    }
}
