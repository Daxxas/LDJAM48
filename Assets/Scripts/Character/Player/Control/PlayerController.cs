using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerController : CharacterController
{
    private InputManager inputManager;

    private Vector2 inputDirection;
    
    [SerializeField] private LayerMask whatIsEnemy;

    
    
    
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        
        inputManager.playerInputs.Gameplay.Movement.performed += context => inputDirection = context.ReadValue<Vector2>();
        inputManager.playerInputs.Gameplay.Movement.canceled += context => inputDirection = context.ReadValue<Vector2>();

        inputManager.playerInputs.Gameplay.Attack.performed += context => Attack();
    }

    private void Update()
    {
        if (IsAttacking)
        {
            rigidbody.velocity = Vector2.zero;
        }
        else
        {
            Move(inputDirection);
        }
    }
    
    private void Move(Vector2 direction)
    {
        if(!IsAttacking)
        {
            this.direction = direction;
            
            if (direction.magnitude > 0.1f)
            {
                rigidbody.velocity = direction * moveSpeed;
            }
            else
            {
                rigidbody.velocity = Vector2.zero;
            }
        }
    }

    private void Attack()
    {
        if (!IsAttacking)
        {
            GetComponentInChildren<Weapon>().Attack(whatIsEnemy);
        }
        
        RaiseAttackEvent();
    }
}
