using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private NavMeshSurface2d navMesh;
    
    private LevelModule[] levels;
    private List<LevelModule> generatedBonusRooms = new List<LevelModule>();

    private void Start()
    {
        levels = Resources.LoadAll<LevelModule>("Levels");
        GenerateLevel(LevelBiome.Plains, 10);
    }

    private void GenerateLevel(LevelBiome levelBiome, int size)
    {
        LevelModule[] levelsForBiome =
            Array.FindAll(levels, level => level.GetComponent<LevelModule>().levelBiome == levelBiome);

        LevelModule[] bottoms = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Bottom);
        LevelModule[] middles = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Middle);
        LevelModule[] bonuses = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Bonus);
        LevelModule[] tops = Array.FindAll(levelsForBiome,
            level => level.GetComponent<LevelModule>().levelType == LevelType.Top);

        GenerateTopRoom(tops);
        GenerateMiddleAndBonusRooms(middles, bonuses, size);
        GenerateBottomRoom(bottoms, size);
        
        navMesh.BuildNavMesh();
    }
    
    private void GenerateTopRoom(LevelModule[] tops)
    {
        foreach (GameObject topGameObject in tops[Random.Range(0, tops.Length)].GetLevelModuleData())
        {
            Instantiate(
                topGameObject,
                new Vector2(0, 0) + (Vector2) topGameObject.transform.position,
                Quaternion.identity,
                transform
            );
        }
    }
    
    private void GenerateMiddleAndBonusRooms(LevelModule[] middles, LevelModule[] bonuses, int size)
    {
        for (int i = 1; i <= size; i++)
        {
            if (Random.value > .9)
            {
                bool hasBonusRoomBeenGenerated = GenerateBonusRoom(bonuses, i);
                if (hasBonusRoomBeenGenerated)
                {
                    continue;
                }
            }
            
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
    }

    private void GenerateBottomRoom(LevelModule[] bottoms, int size)
    {
        foreach (GameObject bottomGameObject in bottoms[Random.Range(0, bottoms.Length)].GetLevelModuleData())
        {
            Instantiate(
                bottomGameObject,
                new Vector2(0, -16 * (size + 1)) + (Vector2) bottomGameObject.transform.position,
                Quaternion.identity,
                transform
            );
        }
    }

    private bool GenerateBonusRoom(LevelModule[] bonuses, int i)
    {
        LevelModule bonusRoom = bonuses[Random.Range(0, bonuses.Length)];
        
        if (!generatedBonusRooms.Contains(bonusRoom))
        {
            generatedBonusRooms.Add(bonusRoom);
            
            foreach (GameObject middleGameObject in bonusRoom.GetLevelModuleData())
            {
                Instantiate(
                    middleGameObject,
                    new Vector2(0, -16 * i) + (Vector2) middleGameObject.transform.position,
                    Quaternion.identity,
                    transform
                );
            }
            
            return true;
        }

        return false;
    }
}