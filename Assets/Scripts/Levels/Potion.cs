using System;
using UnityEngine;

public class Potion : MonoBehaviour, Interactable
{
    [SerializeField] private int healAmount;
    private AudioSource audioSource;
    private AudioClip pickupSound;

    private void Start()
    {
        LoadSounds();
    }

    public void Interact(CharacterController characterController)
    {
        characterController.Heal(healAmount);
        audioSource.PlayOneShot(pickupSound, .5F);
        Destroy(gameObject);
    }
    
    private void LoadSounds()
    {
        audioSource = FindObjectOfType<AudioSource>();
        pickupSound = Resources.Load<AudioClip>("Audio/PickupPotion");
    }
}
