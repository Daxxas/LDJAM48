using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    private LevelModule[] levels;

    [SerializeField] private NavMeshSurface2d navMesh;
    
    private void Start()
    {
        levels = Resources.LoadAll<LevelModule>("Levels");
        GenerateLevel(LevelBiome.Plains, 1);
    }

    private void GenerateLevel(LevelBiome levelBiome, int size)
    {
        LevelModule[] levelsForBiome =
            Array.FindAll(levels, level => level.GetComponent<LevelModule>().levelBiome == levelBiome);

        LevelModule[] bottoms = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Bottom);
        LevelModule[] middles = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Middle);
        LevelModule[] tops = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Top);

        foreach (GameObject topGameObject in tops[Random.Range(0, tops.Length)].GetLevelModuleData())
        {
            Instantiate(
                topGameObject,
                new Vector2(0, 0) + (Vector2) topGameObject.transform.position,
                Quaternion.identity,
                transform
            );
        }

        for (int i = 1; i <= size; i++)
        {
            foreach (GameObject middleGameObject in middles[Random.Range(0, middles.Length)].GetLevelModuleData())
            {
                Instantiate(
                    middleGameObject,
                    new Vector2(0, -16 * i) + (Vector2) middleGameObject.transform.position,
                    Quaternion.identity,
                    transform
                );
            }
        }
        
        foreach (GameObject bottomGameObject in bottoms[Random.Range(0, bottoms.Length)].GetLevelModuleData())
        {
            Instantiate(
                bottomGameObject,
                new Vector2(0, -16 * (size + 1)) + (Vector2) bottomGameObject.transform.position,
                Quaternion.identity,
                transform
            );
        }
        
        navMesh.BuildNavMesh();
    }
}