using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterController : MonoBehaviour
{
    private WeaponType currentWeaponType = WeaponType.Default;

    public WeaponType CurrentWeaponType => currentWeaponType;

    public CharacterState characterState;
    
    protected Rigidbody2D rigidbody;
    
    [SerializeField] protected float moveSpeed = 1f;

    [SerializeField] public AnimationClip attackClip;
    [SerializeField] public AnimationClip hitClip;
    [SerializeField] private float invincibleDurationAfterHit = 0f;
    
    [SerializeField] protected LayerMask whatIsEnemy;
    private int health;

    public int Health => health;
    
    [SerializeField] private int maxHealth = 1;
    
    public float AttackSpeed => attackClip.length;
    
    protected Vector2 direction;
    public Vector2 Direction => direction;
    
    public float CurrentSpeed => rigidbody.velocity.magnitude;
    
    public delegate void OnAttack();
    public event OnAttack onAttack;
    
    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;
    
    public delegate void OnHit();
    public event OnHit onHit;

    
    private bool isHitten = false;
    public bool IsHitten => isHitten;

    public float hitDuration => hitClip.length;
    
    public delegate void OnDeath();
    public event OnDeath onDeath;

    private bool isDead = false;
    public bool IsDead => isDead;
    private bool isInvincible = false;
    
    [SerializeField] private float momentumCoef = 5f;

    protected virtual void Start()
    {
        health = maxHealth;
        currentWeaponType = GetComponentInChildren<Weapon>().WeaponType;
    }

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            if (momentum.magnitude > 0.2f)
            {
                momentum = Vector2.Lerp(momentum, Vector3.zero, momentumCoef * Time.deltaTime);
                rigidbody.velocity = momentum;
            }
        }
    }

    protected void RaiseAttackEvent()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Invoke(nameof(EndAttack), AttackSpeed);
        }
    }

    private void EndAttack()
    {
        onAttack?.Invoke();
        isAttacking = false;
    }

    private Vector2 momentum = Vector2.zero;
    
    public virtual void Hit(Vector2 source, int damage, float knockbackForce)
    {
        if (!isHitten && !isDead && !isInvincible)
        {
            health -= damage;

            if (health <= 0)
            {
                onDeath?.Invoke();
                rigidbody.isKinematic = true;
                isDead = true;
                health = 0;
                return;
            }

            
            isHitten = true;
            var knockbackDirection = (Vector2) transform.position - source;
            AddImpact(knockbackDirection, knockbackForce);
            onHit?.Invoke();
            Invoke(nameof(EndHit), hitDuration);
        }
    }
    
    private void EndHit()
    {
        isHitten = false;
        isInvincible = true;
        Invoke(nameof(StopInvincibleAfterHit), invincibleDurationAfterHit);
    }

    private void StopInvincibleAfterHit()
    {
        isInvincible = false;
    }

    public void AddImpact(Vector2 impactDirection, float force)
    {
        Debug.Log($"AddImpact called with calculated force : {impactDirection.normalized} * {force} = {impactDirection.normalized * force}");
        momentum += impactDirection.normalized * force;
    }


    public void Heal(int healAmount)
    {
        health = Mathf.Clamp(health + healAmount, 0, maxHealth);
    }

    public void UpdateAttackAnimationClip(AnimationClip newClip)
    {
        attackClip = newClip;
    }
    
    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            UnityEditor.Handles.color = Color.white;
            var position = new Vector3(GetComponent<Collider2D>().bounds.center.x, transform.position.y + GetComponent<Collider2D>().bounds.extents.y + 0.5f, transform.position.z);
            UnityEditor.Handles.Label(position, health + "/" + maxHealth + '\n' + rigidbody?.velocity.ToString(), new GUIStyle() {fontSize = 20, normal = new GUIStyleState() {textColor = Color.white}});
        }
#endif
    }
}
