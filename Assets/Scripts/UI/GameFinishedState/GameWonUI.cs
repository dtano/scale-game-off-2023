using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWonUI : UIElement
{
    [SerializeField] private TextMeshProUGUI _timeTakenText;
    [SerializeField] private TextMeshProUGUI _percentageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInformation(string percentageText, string timeTaken)
    {
        _percentageText.text = percentageText;
        _timeTakenText.text = timeTaken;
    }
}
