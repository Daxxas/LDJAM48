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
    [SerializeField] private float attackDashForce = 0f; 
    
    void Start()
    {
        base.Start();
        
         characterState = CharacterState.IDLE;
         targetController = GameObject.FindObjectOfType<PlayerController>(); 
         target = targetController?.transform; 
    }

    void Update()
    {
        base.Update();
        
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

        if (target == null)
            return;
        
        direction = (target.position - transform.position).normalized;

        
        if (Vector2.Distance(transform.position, target.position) >= stoppingDistance)
        {
            if (Vector2.Distance(transform.position, target.position) < eyeReach)
            {
                characterState = CharacterState.WALK;
                // transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                Vector2 toTarget = (target.position - transform.position).normalized; // this has length 1

                rigidbody.velocity = toTarget * moveSpeed;
            }
            else
            {
                rigidbody.velocity = Vector2.zero;
                characterState = CharacterState.IDLE;

            }
        }
        else
        {
            if (!IsAttacking && targetController.Health > 0)
            {
                rigidbody.velocity = Vector2.zero;
                RaiseAttackEvent();
                characterState = CharacterState.ATTACK;
            }
        }
    }

    private void Attack()
    {
        GetComponentInChildren<Weapon>().Attack(whatIsEnemy);
    }

    private void Charge()
    {
        Vector2 toTarget = (target.position - transform.position).normalized;
        AddImpact(toTarget, attackDashForce);
    }
}
