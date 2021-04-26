using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentBiomeIndex = 0; 
    public List<LevelBiome> levelBiomeOrder; 
    public List<int> levelBiomeSize;
    
    private static LevelManager _instance;

    
    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Debug.Log("Start method level manager");
        var levelGenerator = FindObjectOfType<LevelGenerator>();
        if (levelGenerator != null)
        {
            Debug.Log("generate level called");
            levelGenerator.GenerateLevel(levelBiomeOrder[currentBiomeIndex], levelBiomeSize[currentBiomeIndex]);
        }
    }

    
    [ContextMenu("Switch to next level")]
    public void SwitchToNextLevel()
    {
        currentBiomeIndex++;
        SceneManager.LoadScene(1);
    }
    
    
    
    public static LevelManager Instance
    {
        get {
            if (_instance == null)
            {
                GameObject container = new GameObject("LevelManager");
                _instance = container.AddComponent<LevelManager>();
            }
     
            return _instance;
        }
    }
}
