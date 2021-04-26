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

    void Awake()
    {
        _instance = this;
        LoadSounds();
    }

    [ContextMenu("Switch to next level")]
    public void SwitchToNextLevel()
    {
        audioSource.PlayOneShot(nextLevelSound, .5F);

        currentBiomeIndex++;
        SceneManager.LoadScene(1);
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