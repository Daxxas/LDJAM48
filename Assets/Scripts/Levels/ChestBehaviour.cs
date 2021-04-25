using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ChestBehaviour : MonoBehaviour, Interactable
{
    [SerializeField] private LootTable lootTable;
    [SerializeField] private Sprite openSprite;

    private bool isOpened;

    public void Interact(CharacterController characterController)
    {
        if (isOpened) return;

        Random random = new Random();
        int totalWeight = lootTable.weightedLoots.Sum(loot => loot.probabilityWeight);
        int randomPick = random.Next(totalWeight) + 1;

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
        Instantiate(
            loot.item,
            new Vector2(transform.position.x, transform.position.y - 1),
            loot.item.transform.rotation
        );
    }
}