using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMonsterController : CharacterController
{
    private CharacterState CharacterState => characterState;
    private Transform target;
    private PlayerController targetController;

    [SerializeField] private float eyeReach = 10f;
    [SerializeField] private float stoppingDistance = 1f;
    
    void Start()
    {
        base.Start();
        
         characterState = CharacterState.IDLE;
         targetController = GameObject.FindObjectOfType<PlayerController>(); 
         target = targetController.transform; 
    }

    void Update()
    {
        if (IsDead)
        {
            characterState = CharacterState.DEAD;
            return;
        }
        
        if (IsHitten)
        {
            characterState = CharacterState.HIT;
            return;
        }

        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            if (Vector2.Distance(transform.position, target.position) < eyeReach)
            {
                direction = (target.position - transform.position).normalized;
                characterState = CharacterState.WALK;
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                characterState = CharacterState.IDLE;

            }
        }
        else
        {
            if (!IsAttacking && targetController.Health > 0)
            {
                Debug.Log("Starting attack...");
                RaiseAttackEvent();
                characterState = CharacterState.ATTACK;
            }
        }
    }

    private void Attack()
    {
        GetComponentInChildren<Weapon>().Attack(whatIsEnemy);
    }
}
