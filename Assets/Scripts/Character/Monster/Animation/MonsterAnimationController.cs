using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationController : AnimationController
{
    private static readonly string IDLE = "idle";
    
    private static readonly List<string> WALK = new List<string>()
    {
        "walk_up",
        "walk_right",
        "walk_down",
        "walk_left"
    };

    private static readonly List<string> ATTACK = new List<string>()
    {
        "attack_up",
        "attack_right",
        "attack_down",
        "attack_left"
    };

    void Update()
    {
        base.Update();
        
        switch (characterController.characterState)
        {
            case CharacterState.IDLE:
                Debug.Log("IDLE ");

                ChangeAnimationState(IDLE);
                break;
            case CharacterState.WALK:
                ChangeAnimationState(WALK[currentDirection]);
                Debug.Log("WALKING " + WALK[currentDirection]);
                break;
            case CharacterState.ATTACK:
                ChangeAnimationState(ATTACK[currentDirection]);
                break;
            default:
                ChangeAnimationState(IDLE);
                break;
        }
        
    }
}
