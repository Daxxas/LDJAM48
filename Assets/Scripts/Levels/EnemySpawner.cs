using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyTypes;
    [SerializeField] private int min;
    [SerializeField] private int max;
    [SerializeField] private float radius;

    void Start()
    {
        int enemyCount = Random.Range(min, max + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y)
                                    + UnityEngine.Random.insideUnitCircle * radius;
            int enemyTypeIndex = Random.Range(0, enemyTypes.Count);

            Instantiate(
                enemyTypes[enemyTypeIndex],
                enemyPosition,
                enemyTypes[enemyTypeIndex].transform.rotation
            );
        }
    }
}