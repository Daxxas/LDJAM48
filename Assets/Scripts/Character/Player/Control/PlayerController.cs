using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerController : CharacterController
{
    [SerializeField] private LayerMask interactableLayerMask;
    private AudioSource audioSource;

    private InputManager inputManager;
    private Vector2 inputDirection;
    private CharacterController characterController;
    
    private AudioClip runSound;
    private AudioClip hitSound;
    private AudioClip dieSound;
    private float runningSoundCooldown = .3F;
    private float nextRunningSound;

    void Start()
    {
        base.Start();
        
        characterController = GetComponent<CharacterController>();

        InitializeInputs();
        LoadSounds();
    }

    private void Update()
    {
        Vector2 currentVel = Vector2.zero;
        
        if (IsDead || IsAttacking)
        {
            rigidbody.velocity = Vector2.zero;
            return;
        }

        
        if (!IsDead)
        {
            if (momentum.magnitude > 0.2f)
            {
                momentum = Vector2.Lerp(momentum, Vector3.zero, momentumCoef * Time.deltaTime);
            }
            else
            {
                momentum = Vector2.zero;
            }
        }
        else
        {
            momentum = Vector2.zero;
        }
        currentVel = Move(inputDirection) + momentum;

        rigidbody.velocity = currentVel;
    }

    private Vector2 Move(Vector2 direction)
    {
        if (!IsAttacking)
        {
            this.direction = direction;

            if (direction.magnitude > 0.1f)
            {
                isWalking = true;
                PlayRunningSound();
                return direction * moveSpeed;
            }

            isWalking = false;
        }
        return Vector2.zero;
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

    public override void Hit(Vector2 source, int damage, float knockbackForce)
    {
        base.Hit(source, damage, knockbackForce);
        
        audioSource.PlayOneShot(hitSound);
        if (Health <= 0 && !IsDead)
        {
            audioSource.PlayOneShot(dieSound);
        }
    }

    private void LoadSounds()
    {
        audioSource = FindObjectOfType<AudioSource>();
        runSound = Resources.Load<AudioClip>("Audio/Run");
        hitSound = Resources.Load<AudioClip>("Audio/PlayerHit");
        dieSound = Resources.Load<AudioClip>("Audio/Die");
    }

    private void InitializeInputs()
    {
        inputManager = GetComponent<InputManager>();

        inputManager.playerInputs.Gameplay.Movement.performed +=
            context => inputDirection = context.ReadValue<Vector2>();
        inputManager.playerInputs.Gameplay.Movement.canceled +=
            context => inputDirection = context.ReadValue<Vector2>();

        inputManager.playerInputs.Gameplay.Attack.performed += context => PlayerAttack();

        inputManager.playerInputs.Gameplay.Interact.performed += context => Interact();
    }

    private void PlayRunningSound()
    {
        float time = Time.time;
        if (time > nextRunningSound)
        {
            nextRunningSound = time + runningSoundCooldown;
            audioSource.PlayOneShot(runSound, .05F);
        }
    }
}