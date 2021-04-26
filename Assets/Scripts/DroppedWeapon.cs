using System;
using UnityEditor;
using UnityEngine;

public class DroppedWeapon : MonoBehaviour, Interactable
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
    }

    public void Interact(CharacterController characterController)
    {
        GameObject characterWeapon = GetCharacterCurrentWeapon(characterController);
        PutDroppedWeaponOnPlayer(characterController);
        DropPlayerWeapon(characterWeapon);
    }

    private void PutDroppedWeaponOnPlayer(CharacterController characterController)
    {
        GameObject equippedWeapon = Instantiate(
            transform.GetChild(0).gameObject,
            characterController.transform
        );
        equippedWeapon.SetActive(true);
        Destroy(transform.GetChild(0).gameObject);
    }

    private void DropPlayerWeapon(GameObject characterWeapon)
    {
        if (characterWeapon != null)
        {
            GameObject droppedWeapon = Instantiate(characterWeapon, transform);
            droppedWeapon.SetActive(false);
            SetSprite(characterWeapon);
            Destroy(characterWeapon);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private GameObject GetCharacterCurrentWeapon(CharacterController characterController)
    {
        foreach (Transform child in characterController.transform)
        {
            if (child.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                return child.gameObject;
            }
        }

        return null;
    }

    private void SetSprite()
    {
        spriteRenderer.sprite = transform.GetChild(0).GetComponent<Weapon>().DroppedSprite;
    }
    
    private void SetSprite(GameObject weapon)
    {
        spriteRenderer.sprite = weapon.GetComponent<Weapon>().DroppedSprite;
    }
}