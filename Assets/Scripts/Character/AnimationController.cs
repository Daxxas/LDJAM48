using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationController : MonoBehaviour
{
    protected CharacterController characterController;
    protected Animator animator;
    
    protected string currentAnimation;
    protected int currentDirection = 1;
    protected int currentAttackDirection = 1;
    
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    protected void Update()
    {
        UpdateCurrentDirection();
        UpdateAttacktDirection();
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
            if (normalizedDirection.x >= 0)
            {
                currentDirection = 1;
            }
            else
            {
                currentDirection = 3;
            }
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
    
    protected void UpdateAttacktDirection()
    {
        var normalizedDirection = characterController.Direction.normalized;
        
        if (normalizedDirection.y > 0.75f)
        {
            currentAttackDirection = 0;
        }
        else if (normalizedDirection.y < -0.75f)
        { 
            currentAttackDirection = 2;
        }
        else if (normalizedDirection.x > 0.75f)
        {
            currentAttackDirection = 1;
        }
        else if (normalizedDirection.x < -0.75f)
        {
            currentAttackDirection = 3;
        }
    }
    
    protected void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    public void StartFlashing(Color color, float duration, float frequence)
    {
        
        if (frequence <= 0)
        {
            StartCoroutine(FlashColor(color, duration, 0.1f));
        }
        else
        {
            StartCoroutine(FlashColor(color, duration, frequence));
        }
        
    }
    
    private IEnumerator FlashColor(Color color, float duration, float frequence)
    {
        Debug.Log("FlashColor called with parameters : " + duration + " " + frequence);
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var initColor = spriteRenderer.color;

        bool isNewColor = false;
        float timePassed = 0;

        while (timePassed < duration)
        {
            Debug.Log("while iteration");
            spriteRenderer.color = isNewColor? initColor : color;
            isNewColor = !isNewColor;
            yield return new WaitForSeconds(frequence);
            timePassed += frequence;
        }
        spriteRenderer.color =  initColor;

    }
}
