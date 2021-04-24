using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyTypes;
    [SerializeField] private int min;
    [SerializeField] private int max;
    [SerializeField] private float radius;

    void Start()
    {
        Random random = new Random();
        int enemyCount = random.Next(min, max + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y)
                                    + UnityEngine.Random.insideUnitCircle * radius;
            int enemyTypeIndex = random.Next(enemyTypes.Count);

            Instantiate(
                enemyTypes[enemyTypeIndex],
                enemyPosition,
                enemyTypes[enemyTypeIndex].transform.rotation
            );
        }
    }
}