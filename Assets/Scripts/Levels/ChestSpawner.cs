using UnityEngine;
using Random = UnityEngine.Random;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject chest;
    [SerializeField] [Range(0f, 1f)] private float spawnProbability;

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
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}