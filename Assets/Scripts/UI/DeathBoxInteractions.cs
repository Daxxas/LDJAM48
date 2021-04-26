using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBoxInteractions : MonoBehaviour
{
    private void Start()
    {
        var player = FindObjectOfType<PlayerController>();

        player.onDeath += (AppearOnPlayerDeath);
    }
    

    public void LoadSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void AppearOnPlayerDeath()
    {
        Debug.Log("Death box appear");
        transform.GetChild(0).gameObject.SetActive(true);
    }
    
}
