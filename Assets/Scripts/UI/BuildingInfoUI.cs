using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _floorsValueText;

    public void SetInformation(BuildingDataSO buildingData)
    {
        _buildingNameText.text = buildingData.Name;
        _floorsValueText.text = (buildingData.NumFloors > 0 ? buildingData.NumFloors - 1 : 0).ToString();
    }
}
