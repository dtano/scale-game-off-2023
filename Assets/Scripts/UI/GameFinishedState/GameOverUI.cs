using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : UIElement
{
    [SerializeField] private TextMeshProUGUI _percentageText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInformation(string percentageText)
    {
        _percentageText.text = percentageText;
    }
}
