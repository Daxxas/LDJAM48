using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerController : CharacterController
{
    [SerializeField] private LayerMask chestLayerMask;

    private InputManager inputManager;
    private Vector2 inputDirection;

    void Start()
    {
        base.Start();

        inputManager = GetComponent<InputManager>();

        inputManager.playerInputs.Gameplay.Movement.performed +=
            context => inputDirection = context.ReadValue<Vector2>();
        inputManager.playerInputs.Gameplay.Movement.canceled +=
            context => inputDirection = context.ReadValue<Vector2>();

        inputManager.playerInputs.Gameplay.Attack.performed += context => Attack();

        inputManager.playerInputs.Gameplay.OpenChest.performed += context => OpenChest();
    }

    private void Update()
    {
        base.Update();

        if (IsDead || IsHitten || IsAttacking)
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

    private void OpenChest()
    {
        Collider2D chest = Physics2D.OverlapCircle(transform.position, 1F, chestLayerMask.value);
        if (chest != null)
        {
            chest.GetComponent<ChestBehaviour>().Open();
        }
    }
}