using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorIndicator : MonoBehaviour
{
    private const string IS_MOVING_ANIMATION_PARAM = "isMoving";
    private const string SWITCH_DIRECTION_ANIMATION_PARAM = "switchDirection";
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StopMovement()
    {
        _animator.SetBool(IS_MOVING_ANIMATION_PARAM, false);
    }

    public void StartMovement()
    {
        _animator.SetBool(IS_MOVING_ANIMATION_PARAM, true);
    }

    public void SwitchDirection()
    {
        _animator.SetTrigger(SWITCH_DIRECTION_ANIMATION_PARAM);
        StopMovement();
    }
}
