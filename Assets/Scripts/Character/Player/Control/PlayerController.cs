using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerController : CharacterController
{
    [SerializeField] private LayerMask interactableLayerMask;

    private InputManager inputManager;
    private Vector2 inputDirection;
    private CharacterController characterController;

    void Start()
    {
        base.Start();
        
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();

        inputManager.playerInputs.Gameplay.Movement.performed +=
            context => inputDirection = context.ReadValue<Vector2>();
        inputManager.playerInputs.Gameplay.Movement.canceled +=
            context => inputDirection = context.ReadValue<Vector2>();

        inputManager.playerInputs.Gameplay.Attack.performed += context => PlayerAttack();

        inputManager.playerInputs.Gameplay.Interact.performed += context => Interact();
    }

    private void Update()
    {
        base.Update();

        if (IsDead || IsAttacking)
        {
            rigidbody.velocity = Vector2.zero;
            return;
        }

        Move(inputDirection);
    }

    private void Move(Vector2 direction)
    {
        if (!IsAttacking)
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

    private void PlayerAttack()
    {
        if (!IsDead && !IsHitten)
        {
            RaiseAttackEvent();
        }
    }

    private void Interact()
    {
        Debug.Log("interact called");
        Collider2D[] interactables = Physics2D.OverlapCircleAll(transform.position, 1F, interactableLayerMask.value);
        if (interactables.Length > 0)
        {
            Debug.Log("interact found");
            Collider2D closest = interactables[0];
            float closestDistance = float.MaxValue;
            
            foreach (Collider2D interactable in interactables)
            {
                float distance = Vector3.Distance(transform.position, interactable.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }
            }

            closest.GetComponent<Interactable>().Interact(characterController);
        }
    }
}