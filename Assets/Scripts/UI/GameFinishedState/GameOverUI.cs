using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : UIElement
{
    [SerializeField] private TextMeshProUGUI _percentageText;

    public void SetInformation(string percentageText)
    {
        _percentageText.text = percentageText;
    }
}
