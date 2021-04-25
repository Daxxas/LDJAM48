using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    private static readonly List<string> IDLE = new List<string>()
    {
        "Idle_up_right",
        "Idle_right",
        "Idle_up_left",
        "Idle_left"
    };

    
    private static readonly List<string> WALK = new List<string>()
    {
        "Walk_up_right",
        "Walk_right",
        "Walk_up_left",
        "Walk_left"
    };

    private static readonly List<string> SLASH_ATTACK = new List<string>()
    {
        "Slash_up_right",
        "Slash_right",
        "Slash_up_left",
        "Slash_left"
    };
    
    private static readonly List<string> THRUST_ATTACK = new List<string>()
    {
        "Thrust_up_right",
        "Thrust_right",
        "Thrust_up_left",
        "Thrust_left"
    };

    
    private static readonly List<string> HIT = new List<string>()
    {
        "Hit_up_right",
        "Hit_right",
        "Hit_up_left",
        "Hit_left"
    };

    private static readonly string DEATH = "dead";
    
    private void Start()
    {
        base.Start();
    }

    
    private void Update()
    {
        base.Update();

        if (characterController.IsDead)
        {
            ChangeAnimationState(DEATH);
            return;
        }
        
        if (characterController.IsHitten)
        {
            ChangeAnimationState(HIT[currentDirection]);
            return;
        }
        
        if (!characterController.IsAttacking)
        {
            if (characterController.CurrentSpeed < 0.1f)
            {
                ChangeAnimationState(IDLE[currentDirection]);
            }
            else
            {
                ChangeAnimationState(WALK[currentDirection]);
            }
        }
        else
        {
            switch (characterController.CurrentWeaponType)
            {
                case WeaponType.Slash:
                    ChangeAnimationState(SLASH_ATTACK[currentAttackDirection]);
                    break;
                case WeaponType.Thrust:
                    ChangeAnimationState(THRUST_ATTACK[currentAttackDirection]);
                    break;
                default:
                    break;
            }
        }
    }

}
