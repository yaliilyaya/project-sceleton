using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterAssetsThirdPersonAnimationController : MonoBehaviour
{
    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    
    private Animator _animator;
    private bool _hasAnimator;
    
    private bool isMove;
    private bool isStep;

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        AssignAnimationIDs();
        _hasAnimator = TryGetComponent(out _animator);
    }

    // Update is called once per frame
    void Update()
    {
        _hasAnimator = TryGetComponent(out _animator);
    }


    public void SetSpeed(float speed)
    {
        if (_hasAnimator)
        {
            //isMove = speed > 0;
            _animator.SetFloat(_animIDSpeed, speed);
        }
    }
    
    public void SetMotionSpeed(float inputMagnitude)
    {
        if (_hasAnimator)
        {
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }
    }
    
    public void SetGrounded(bool grounded)
    {
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDGrounded, grounded);
        }
    }

    public void SetJump(bool isJump)
    {
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDJump, isJump);
        }
    }

    public void SetFreeFall(bool isFreeFall)
    {
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDFreeFall, isFreeFall);
        }
    }
    
    
    private void OnFootstep(AnimationEvent animationEvent)
    {
        //TODO:: Здесь должен быть проигран звук

        isStep = !isStep;
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        //TODO:: Здесь должен быть проигран звук
    }

    private void OnDrawGizmos()
    {
        if (isStep)
        {
            // Gizmos.color = Color.green;
            // Gizmos.DrawRay(transform.position, transform.forward);
        }
        else
        {
            // Gizmos.color = Color.red;
            // Gizmos.DrawRay(transform.position, transform.forward);
        }
      
    }
}
