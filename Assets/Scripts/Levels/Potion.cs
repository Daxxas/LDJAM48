using UnityEngine;

public class Potion : MonoBehaviour, Interactable
{
    [SerializeField] private int healAmount;
    
    public void Interact(CharacterController characterController)
    {
        characterController.Heal(healAmount);
        Destroy(gameObject);
    }
}
