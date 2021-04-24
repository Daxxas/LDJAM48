using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : CharacterController
{
    [SerializeField] private float moveSpeed = 1f;
    
    private Rigidbody2D rigidbody;
    private InputManager inputManager;

    void Awake()
    {
        inputManager.playerInputs.Gameplay.Movement.performed += context => Move(context.ReadValue<Vector2>());
        inputManager.playerInputs.Gameplay.Movement.canceled += context => Move(context.ReadValue<Vector2>());
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        inputManager = GetComponent<InputManager>();
    }
    
    private void Move(Vector2 direction)
    {
        this.direction = direction;
        
        Debug.Log(direction.magnitude);
        if (direction.magnitude > 0.1f)
        {
            rigidbody.velocity = direction * moveSpeed;
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    private void Attack()
    {
        
    }
}
