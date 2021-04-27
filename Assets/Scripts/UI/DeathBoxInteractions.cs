using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBoxInteractions : MonoBehaviour
{
    private PlayerController player;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        player.onDeath += AppearOnPlayerDeath;
    }
    

    public void LoadSceneIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    [ContextMenu("appear")]
    private void AppearOnPlayerDeath()
    {
        
        Debug.Log("Death box appear");
        
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
        LeanTween.scale(transform.GetChild(0).gameObject, new Vector3(1f, 1f, 1f), 0.5f);
        
        Destroy(player.gameObject);

    }


    public void OnDestroy()
    {
        player.onDeath -= AppearOnPlayerDeath;
    }
}
