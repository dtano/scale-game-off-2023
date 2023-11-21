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
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StopMovement()
    {
        _animator.SetBool("isMoving", false);
    }

    public void StartMovement()
    {
        _animator.SetBool("isMoving", true);
    }

    public void SwitchDirection()
    {
        Debug.Log("SWITCH DIRECTION");
        _animator.SetTrigger("switchDirection");
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
