using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloorNumberIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _floorNumberText;
    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFloorNumber(int floorNumber)
    {
        if(floorNumber == 0)
        {
            _floorNumberText.text = "G";
            return;
        }
        _floorNumberText.text = floorNumber.ToString();
    }
}
