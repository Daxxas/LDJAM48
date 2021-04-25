using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationController : AnimationController
{
    private const string IDLE  = "idle";
    private static readonly List<string> ATTACK = new List<string>()
    {
        "Attack_up",
        "Attack_right",
        "Attack_down",
        "Attack_left"
    };

    protected override void Start()
    {
        animator = GetComponentInParent<Animator>();
        characterController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        base.Update();
        
        if (!characterController.IsAttacking)
        {
            ChangeAnimationState(IDLE);
        }
        else
        {
            ChangeAnimationState(ATTACK[currentDirection]);
        }
    }
    
}