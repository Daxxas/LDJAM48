using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationController : MonoBehaviour
{
    protected CharacterController characterController;
    protected Animator animator;

    protected string currentAnimation;
    protected int currentDirection = 1;


    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    protected void Update()
    {
        UpdateCurrentDirection();
    }
    
    protected void UpdateCurrentDirection()
    {
        var normalizedDirection = characterController.Direction.normalized;
        
        if (normalizedDirection.y > 0.75f)
        {
            if (normalizedDirection.x >= 0)
            {
                currentDirection = 0;
            }
            else
            {
                currentDirection = 2;
            }
        }
        else if (normalizedDirection.y < -0.75f)
        {
            currentDirection = 2;
        }
        else if (normalizedDirection.x > 0.75f)
        {
            currentDirection = 1;
        }
        else if (normalizedDirection.x < -0.75f)
        {
            currentDirection = 3;
        }
    }
    
    protected void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }
    
}
