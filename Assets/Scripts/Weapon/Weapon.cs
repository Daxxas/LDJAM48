using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage;
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
        Debug.Log("Weapon attacking !");
        Collider2D[] result = new Collider2D[3];

        ContactFilter2D filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.layerMask = whatIsEnemy;
        GetComponentInChildren<Collider2D>().OverlapCollider(filter, result);

        foreach (var enemy in result)
        {
            enemy?.GetComponent<CharacterController>()?.Hit(damage);
            Debug.Log("Hitting enemy !");
        }
    }
}
