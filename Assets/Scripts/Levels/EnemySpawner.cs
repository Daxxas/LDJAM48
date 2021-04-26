using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MobTable enemyTypes;
    [SerializeField] private int min;
    [SerializeField] private int max;
    [SerializeField] private float radius;
    [SerializeField] private bool spawnBossLevel = false;
    void Start()
    {
        if (enemyTypes == null)
            return;
        
        int enemyCount = Random.Range(min, max + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y)
                                    + UnityEngine.Random.insideUnitCircle * radius;
            int enemyTypeIndex = Random.Range(0, enemyTypes.table.Count);

            var spawnedEnemy = Instantiate(
                enemyTypes.table[enemyTypeIndex],
                enemyPosition,
                enemyTypes.table[enemyTypeIndex].transform.rotation
            );

            if (spawnBossLevel)
            {
                var exitZone = FindObjectOfType<ExitZone>();
                if (exitZone != null)
                {
                    exitZone.AddConditionBoss(spawnedEnemy.GetComponent<CharacterController>());
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}