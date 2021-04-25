using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestBehaviour : MonoBehaviour, Interactable
{
    [SerializeField] private LootTable lootTable;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private GameObject droppedWeaponPrefab;

    private bool isOpened;

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
}