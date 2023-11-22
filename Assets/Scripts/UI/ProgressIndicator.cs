using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ProgressIndicator : UIElement
{
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TextMeshProUGUI _percentageText;
    [SerializeField] private TextMeshProUGUI _additionIndicator;

    private Animator _animator;
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowAdditionIndicator(int value)
    {
        _additionIndicator.text = $"+{value}";
        if(_animator != null) _animator.SetTrigger("ShowAddition");
    }

    public void UpdateValue(int currentValue)
    {
        if(_additionIndicator != null)
        {
            ShowAdditionIndicator((int) (currentValue - _progressSlider.value));
        }

        _progressSlider.value = currentValue;
        _percentageText.text = $"{_progressSlider.value}/{_progressSlider.maxValue}";
    }

    public void InitValues(int maxValue)
    {
        _progressSlider.value = 0;
        _progressSlider.maxValue = maxValue;

        _percentageText.text = $"0/{maxValue}";
    }
}
