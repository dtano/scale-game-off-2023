using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorIndicator : MonoBehaviour
{
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
        _animator.SetTrigger("switchDirection");
        StopMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
