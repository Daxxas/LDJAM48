using UnityEngine;
using Random = UnityEngine.Random;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject chest;
    [SerializeField] private float spawnProbability;

    private void Start()
    {
        if (spawnProbability > Random.value)
        {
            SpawnChest();
        }
    }

    private void SpawnChest()
    {
        Instantiate(chest, transform.position, Quaternion.identity);
    }
}