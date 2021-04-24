using UnityEngine;


public abstract class CharacterController : MonoBehaviour
{
    protected Vector2 direction;

    public Vector2 Direction => direction;

    protected void Move(Vector2 direction){}
}
