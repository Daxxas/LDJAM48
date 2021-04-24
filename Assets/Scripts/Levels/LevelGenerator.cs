using System;
using UnityEngine;
using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    private GameObject[] levels;

    private void Start()
    {
        levels = Resources.LoadAll<GameObject>("Levels");
        GenerateLevel(LevelBiome.Plains, 2);
    }

    private void GenerateLevel(LevelBiome levelBiome, int size)
    {
        GameObject[] levelsForBiome =
            Array.FindAll(levels, level => level.GetComponent<LevelModule>().levelBiome == levelBiome);

        GameObject[] bottoms = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Bottom);
        GameObject[] middles = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Middle);
        GameObject[] tops = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Top);

        Random random = new Random();

        Instantiate(
            bottoms[random.Next(bottoms.Length)],
            new Vector2(0, 0),
            Quaternion.identity
        );

        for (int i = 1; i <= size; i++)
        {
            Instantiate(
                middles[random.Next(middles.Length)],
                new Vector2(0, 16 * i),
                Quaternion.identity
            );
        }

        Instantiate(
            tops[random.Next(tops.Length)],
            new Vector2(0, 16 * (size + 1)),
            Quaternion.identity
        );
    }
}