using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationController : AnimationController
{
    private static readonly string IDLE = "idle";
    
    private static readonly List<string> WALK = new List<string>()
    {
        "walk_up_right",
        "walk_right",
        "walk_up_left",
        "walk_left"
    };

    private static readonly List<string> ATTACK = new List<string>()
    {
        "attack_up_right",
        "attack_right",
        "attack_up_left",
        "attack_left"
    };
    
    private static readonly List<string> HIT = new List<string>()
    {
        "hit_up_right",
        "hit_right",
        "hit_up_left",
        "hit_left"
    };

    private static readonly string DEATH = "death";

    void Start()
    {
        base.Start();
        
        characterController.onHit += () => StartFlashing(characterController.HitColor, characterController.HitDuration, characterController.HitBlinkFrequence);

    }
    
    void Update()
    {
        base.Update();


        
        switch (characterController.characterState)
        {
            case CharacterState.DEAD:
                ChangeAnimationState(DEATH);
                break;
            case CharacterState.ATTACK:
                ChangeAnimationState(ATTACK[currentDirection]);
                break;
            case CharacterState.IDLE:
                ChangeAnimationState(IDLE);
                break;
            case CharacterState.WALK:
                ChangeAnimationState(WALK[currentDirection]);
                break;
            default:
                ChangeAnimationState(IDLE);
                break;
        }
            
        
    }
}
