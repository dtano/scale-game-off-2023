using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmployeeWeightIcon : MonoBehaviour
{
    [SerializeField] private Image _employeeSprite;
    [SerializeField] private TextMeshProUGUI _weightRangeText;

    public void SetData(BodyTypeDataSO employeeType)
    {
        _employeeSprite.sprite = employeeType.IconSprite;
        _weightRangeText.text = $"{employeeType.MinWeight}kg - {employeeType.MaxWeight}kg";
    }
}
