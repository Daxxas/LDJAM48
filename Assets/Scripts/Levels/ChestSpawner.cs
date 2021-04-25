using UnityEngine;
using Random = System.Random;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject chest;
    [SerializeField] private float spawnProbability;

    private void Start()
    {
        Random random = new Random();
        if (spawnProbability > random.NextDouble())
        {
            SpawnChest();
        }
    }

    private void SpawnChest()
    {
        Instantiate(chest, transform.position, Quaternion.identity);
    }
}