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

        inputManager.playerInputs.Gameplay.Attack.performed += context => Attack();

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

    private void Attack()
    {
        if (!IsDead && !IsHitten)
        {
            if (!IsAttacking)
            {
                GetComponentInChildren<Weapon>().Attack(whatIsEnemy);
            }

            RaiseAttackEvent();
        }
    }

    private void Interact()
    {
        Collider2D interactable = Physics2D.OverlapCircle(transform.position, 1F, interactableLayerMask.value);
        if (interactable != null)
        {
            interactable.GetComponent<Interactable>().Interact(characterController);
        }
    }
}