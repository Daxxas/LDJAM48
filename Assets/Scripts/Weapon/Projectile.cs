using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private Rigidbody2D rigidbody;

    public LayerMask whatIsEnemy;

    public int damage = 0;
    public float knockback = 0;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
        float fRotation = rigidbody.rotation * Mathf.Deg2Rad;
        float fX = Mathf.Sin(fRotation);
        float fY = Mathf.Cos(fRotation);
        Vector2 forward = new Vector2(fY, fX);
        
        rigidbody.velocity = forward * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Wall")))
        {
            Destroy(gameObject);
        }

        if (GetComponent<Collider2D>().IsTouchingLayers(whatIsEnemy.value))
        {
            other.GetComponent<CharacterController>().Hit(transform.position, damage, knockback);
            Destroy(gameObject);
        }
    }
}