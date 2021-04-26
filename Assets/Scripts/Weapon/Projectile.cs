using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private Rigidbody2D rigidbody;

    public LayerMask whatIsEnemy;
    public LayerMask whatIsWall;
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
        Debug.Log("trigger");
        if( whatIsWall == (whatIsWall | (1 << other.GetComponent<Collider2D>().gameObject.layer)))
        {
            Destroy(gameObject);
        }
        
        if( whatIsEnemy == (whatIsEnemy | (1 << other.GetComponent<Collider2D>().gameObject.layer)))
        {
            var characterHit = other.GetComponent<CharacterController>();
            if (characterHit != null && !characterHit.IsDead)
            {
                characterHit.Hit(transform.position, damage, knockback);
                Destroy(gameObject);
            }
        }
    }
}