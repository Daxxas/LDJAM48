using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    private static readonly List<string> IDLE = new List<string>()
    {
        "Idle_up",
        "Idle_right",
        "Idle_right",
        "Idle_left"
    };

    
    private static readonly List<string> WALK = new List<string>()
    {
        "Walk_up",
        "Walk_right",
        "Idle_right",
        "Walk_left"
    };

    private static readonly List<string> SLASH_ATTACK = new List<string>()
    {
        "Slash_up",
        "Slash_right",
        "Slash_down",
        "Slash_left"
    };
    
    private static readonly List<string> THRUST_ATTACK = new List<string>()
    {
        "Thrust_up",
        "Thrust_right",
        "Thrust_down",
        "Thrust_left"
    };

    
    private static readonly List<string> HIT = new List<string>()
    {
        "Hit_up",
        "Hit_right",
        "Hit_right",
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
            Debug.Log("character is attacking");
            switch (characterController.CurrentWeaponType)
            {
                case WeaponType.Slash:
                    ChangeAnimationState(SLASH_ATTACK[currentDirection]);
                    break;
                case WeaponType.Thrust:
                    ChangeAnimationState(THRUST_ATTACK[currentDirection]);
                    break;
                default:
                    break;
            }
        }
    }

}
