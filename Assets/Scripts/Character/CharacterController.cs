using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterController : MonoBehaviour
{
    public CharacterState characterState;
    
    protected Rigidbody2D rigidbody;
    
    [SerializeField] protected float moveSpeed = 1f;

    [SerializeField] public AnimationClip attackClip;

    public float attackSpeed => attackClip.length;
    
    protected Vector2 direction;
    public Vector2 Direction => direction;
    
    public float CurrentSpeed => rigidbody.velocity.magnitude;
    
    public delegate void OnAttack();
    public event OnAttack onAttack;
    
    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;
    
    public delegate void OnHit();
    public event OnHit onHit;

    
    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void RaiseAttackEvent()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            onAttack?.Invoke();
            Invoke(nameof(EndAttack), attackSpeed);
        }
    }

    private void EndAttack()
    {
        isAttacking = false;
    }

    public void Hit(int damage)
    {
        onHit?.Invoke();
    }
}
