using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestBehaviour : MonoBehaviour, Interactable
{
    [SerializeField] private LootTable lootTable;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private GameObject droppedWeaponPrefab;

    private bool isOpened;
    private AudioSource audioSource;
    private AudioClip openChestSound;

    private void Start()
    {
        LoadSounds();
    }

    public void Interact(CharacterController characterController)
    {
        if (isOpened) return;

        int totalWeight = lootTable.weightedLoots.Sum(loot => loot.probabilityWeight);
        int randomPick = Random.Range(0, totalWeight) + 1;

        int accumulatedWeight = 0;
        foreach (Loot loot in lootTable.weightedLoots)
        {
            accumulatedWeight += loot.probabilityWeight;
            if (accumulatedWeight >= randomPick)
            {
                isOpened = true;
                gameObject.layer = LayerMask.NameToLayer("Default");
                GetComponent<SpriteRenderer>().sprite = openSprite;
                audioSource.PlayOneShot(openChestSound, .5F);

                DropLoot(loot);
                break;
            }
        }
    }

    private void DropLoot(Loot loot)
    {
        if (loot.item.layer == LayerMask.NameToLayer("Interactable"))
        {
            Instantiate(
                loot.item,
                new Vector2(transform.position.x, transform.position.y - 1),
                loot.item.transform.rotation
            );
        }
        else if (loot.item.layer == LayerMask.NameToLayer("Weapon"))
        {
            GameObject droppedWeapon = Instantiate(
                droppedWeaponPrefab,
                new Vector2(transform.position.x, transform.position.y - 1),
                droppedWeaponPrefab.transform.rotation
            );
            
            Instantiate(
                loot.item,
                droppedWeapon.transform
            ).SetActive(false);
        }
    }

    private void LoadSounds()
    {
        audioSource = FindObjectOfType<AudioSource>();
        openChestSound = Resources.Load<AudioClip>("Audio/OpenChest");
    }
}