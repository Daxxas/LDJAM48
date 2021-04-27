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

    private AudioSource audioSource;
    private AudioClip nextLevelSound;
    private LevelBiome biomeMusic;

    void Awake()
    {
        _instance = this;
        LoadSounds();
    }

    [ContextMenu("Switch to next level")]
    public void SwitchToNextLevel()
    {
        currentBiomeIndex++;

        if (currentBiomeIndex >= levelBiomeOrder.Count - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            audioSource.PlayOneShot(nextLevelSound, .5F);
            if (levelBiomeOrder[currentBiomeIndex] != biomeMusic)
            {
                biomeMusic = levelBiomeOrder[currentBiomeIndex];
                audioSource.clip = Resources.Load<AudioClip>("Audio/" + biomeMusic);
                audioSource.Play();
            } 
            
            SceneManager.LoadScene(1);

        }
        
        
    }


    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject container = new GameObject("LevelManager");
                _instance = container.AddComponent<LevelManager>();
            }

            return _instance;
        }
    }

    private void LoadSounds()
    {
        audioSource = FindObjectOfType<AudioSource>();
        nextLevelSound = Resources.Load<AudioClip>("Audio/NextLevel");
    }
}