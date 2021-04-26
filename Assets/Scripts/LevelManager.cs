using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentBiomeIndex = 0;
    [SerializeField] private List<LevelBiome> levelBiomeOrder;
    [SerializeField] private List<int> levelBiomeSize;
    
    private static LevelManager _instance;

    
    void Awake()
    {
        _instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        var levelGenerator = FindObjectOfType<LevelGenerator>();
        levelGenerator.GenerateLevel(levelBiomeOrder[currentBiomeIndex], levelBiomeSize[currentBiomeIndex]);
    }

    public void SwitchToNextLevel()
    {
        currentBiomeIndex++;
        SceneManager.LoadScene(1);
        var levelGenerator = FindObjectOfType<LevelGenerator>();
        levelGenerator.GenerateLevel(levelBiomeOrder[currentBiomeIndex], levelBiomeSize[currentBiomeIndex]);
    }
    
    
    
    public static LevelManager Instance
    {
        get {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    GameObject container = new GameObject("LevelManager");
                    _instance = container.AddComponent<LevelManager>();
                }
            }
     
            return _instance;
        }
    }
}
