using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloorNumberIndicator : MonoBehaviour
{
    private const string MOVEMENT_ANIMATION_PARAM = "isMoving";
    private const string ARROW_DIRECTION_ANIMATION_PARAM = "isPointingUp";
    
    [SerializeField] private TextMeshProUGUI _floorNumberText;
    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StopMovement()
    {
        _animator.SetBool(MOVEMENT_ANIMATION_PARAM, false);
    }

    public void StartMovement()
    {
        _animator.SetBool(MOVEMENT_ANIMATION_PARAM, true);
    }

    public void SwitchDirection(bool isPointingUp)
    {
        _animator.SetBool(ARROW_DIRECTION_ANIMATION_PARAM, isPointingUp);
        StopMovement();
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
