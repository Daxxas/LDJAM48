using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SmartMonsterController : CharacterController
{
    private NavMeshAgent agent;
    private Transform target;
    private PlayerController targetController;
    

    [SerializeField] private float eyeReach = 10f;
    [SerializeField] private float attackDashForce = 0f; 

    void Start() {
        base.Start();
        
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        
        targetController = GameObject.FindObjectOfType<PlayerController>(); 
        target = targetController?.transform; 
        
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();

        
        if (IsDead)
        {
            rigidbody.velocity = Vector2.zero;
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
        
        if (Vector2.Distance(transform.position, target.position) >= agent.stoppingDistance)
        {
            if (Vector2.Distance(transform.position, target.position) < eyeReach)
            {
                agent.isStopped = false;

                characterState = CharacterState.WALK;

                agent.SetDestination(target.position);
            }
            else
            {
                characterState = CharacterState.IDLE; 
                agent.isStopped = true;
            }
        }
        else
        {
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            
            if (!IsAttacking && targetController.Health > 0)
            {
                RaiseAttackEvent();
                characterState = CharacterState.ATTACK;
            }
        }
    }
    
    private void Attack()
    {
        GetComponentInChildren<Weapon>().Attack(whatIsEnemy);
    }

    public override void Hit(Vector2 source, int damage, float knockbackForce)
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        
        base.Hit(source, damage, knockbackForce);
    }
    
    
    private void Charge()
    {
        Vector2 toTarget = (target.position - transform.position).normalized;
        AddImpact(toTarget, attackDashForce);
    }
}
