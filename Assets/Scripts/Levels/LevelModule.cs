using System.Collections.Generic;
using UnityEngine;

public class LevelModule : MonoBehaviour
{
    public LevelBiome levelBiome;
    public LevelType levelType;

    public List<GameObject> GetLevelModuleData()
    {
        List<GameObject> result = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            result.Add(transform.GetChild(i).gameObject);
        }

        return result;
    }
}
