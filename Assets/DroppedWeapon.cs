using UnityEngine;

public class DroppedWeapon : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject weapon;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
    }

    public void Interact(CharacterController characterController)
    {
        GameObject currentWeapon = GetCharacterCurrentWeapon(characterController);
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        GameObject instantiatedWeapon = Instantiate(
            weapon,
            characterController.transform.position,
            weapon.transform.rotation
        );
        instantiatedWeapon.transform.parent = characterController.transform;
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
        spriteRenderer.sprite = weapon.GetComponent<Weapon>().DroppedSprite;
    }
}