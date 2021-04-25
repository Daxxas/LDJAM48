using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;

    public WeaponType WeaponType => weaponType;
    
    [SerializeField] private int damage = 0;
    
    [Header("Weapon Shot Options")] [SerializeField]
    private GameObject projectile;
    
    [Header("Weapon Close Options")]
    [SerializeField] private float knockback = 0;
    [SerializeField] private Transform hitZone;
    
    private CharacterController characterController;

    public CharacterController CharacterController => characterController;

    
    void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        FollowCharacterRotation();
    }

    private void FollowCharacterRotation()
    {
        var normalizedDirection = characterController.Direction.normalized;

        if (normalizedDirection != Vector2.zero)
        {
            var angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;
            
            hitZone.transform.eulerAngles = new Vector3(0, 0, angle);

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
                return;
            
            enemy.GetComponent<CharacterController>()?.Hit(transform.position, damage, knockback);
        }
   
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
}
