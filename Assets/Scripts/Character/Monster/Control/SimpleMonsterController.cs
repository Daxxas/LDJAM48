using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMonsterController : CharacterController
{
    private CharacterState CharacterState => characterState;
    private Transform target;

    [SerializeField] private float eyeReach = 10f;
    [SerializeField] private float stoppingDistance = 1f;
    
    void Start()
    {
         characterState = CharacterState.IDLE;
         target = GameObject.FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < eyeReach && Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            direction = (target.position - transform.position).normalized;
            Debug.Log(direction);
            characterState = CharacterState.WALK;
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            characterState = CharacterState.IDLE;
        }
    }
}
