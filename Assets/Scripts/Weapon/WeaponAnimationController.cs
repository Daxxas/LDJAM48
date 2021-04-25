using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponAnimationController : AnimationController
{
    private const string IDLE  = "idle";

    private static readonly List<string> ATTACK = new List<string>()
    {
        "attack_up",
        "attack_right",
        "attack_down",
        "attack_left"
    };

    protected override void Start()
    {
        animator = GetComponentInParent<Animator>();
        characterController = GetComponentInParent<CharacterController>();

        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == ATTACK[0])
            {
                 characterController.UpdateAttackAnimationClip(clip);
                 return;
            }
        }
        
        Debug.Log("Attention : l'arme n'a pas été mise à jour");
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
            ChangeAnimationState(ATTACK[currentAttackDirection]);
        }
    }

   
}