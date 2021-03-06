using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    public GameObject WeaponPrefab => weaponPrefab;
    
    [SerializeField] private WeaponType weaponType;
    public WeaponType WeaponType => weaponType;
    
    [SerializeField] private Sprite droppedSprite;
    public Sprite DroppedSprite => droppedSprite;

    private AudioClip attackSound;
    private AudioSource audioSource;

    
    [SerializeField] private int damage = 0;
    [SerializeField] private float turnSpeed = 0f; 
    
    [Header("Weapon Shot Options")]
    [SerializeField] private GameObject projectile;
    
    [Header("Weapon Close Options")]
    [SerializeField] private float knockback = 0;
    [SerializeField] private Transform hitZone;

    private CharacterController characterController;

    public CharacterController CharacterController => characterController;
    
    void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
        characterController.onAttack += UpdateRotation;
        
        LoadSounds();
    }

    void Update()
    {
        FollowCharacterRotation();
        
    }

    private void FollowCharacterRotation()
    {
        if (!characterController.IsAttacking)
        {
            UpdateRotation();
        }
    }

    private void UpdateRotation()
    {
        var normalizedDirection = characterController.Direction.normalized;

        if (normalizedDirection != Vector2.zero)
        {
            var angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;

            // hitZone.transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, 0, angle, 0), 1);

            if (hitZone != null)
            {
                hitZone.transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }
    public void Attack(LayerMask whatIsEnemy)
    {
        Collider2D[] result = new Collider2D[3];

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(whatIsEnemy);
        
        GetComponentInChildren<Collider2D>().OverlapCollider(filter, result);


        foreach (var enemy in result)
        {
            if (enemy == null)
                break;
            
            enemy.GetComponent<CharacterController>()?.Hit(transform.position, damage, knockback);
        }
        
        audioSource.PlayOneShot(attackSound);
    }

    public void Shoot()
    {
        if (weaponType == WeaponType.Shot)
        {
            var instantiatedProjectile = Instantiate(projectile, transform.position, hitZone.transform.rotation);

            var projectileComp = instantiatedProjectile.GetComponent<Projectile>();
            projectileComp.damage = damage;
            projectileComp.knockback = knockback;
            projectileComp.whatIsEnemy = characterController.whatIsEnemy;
        }
    }

    private void LoadSounds()
    {
        audioSource = FindObjectOfType<AudioSource>();
        attackSound = Resources.Load<AudioClip>("Audio/" + weaponType);
    }
}
