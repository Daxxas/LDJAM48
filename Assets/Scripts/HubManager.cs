using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Start()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            var newPlayer = Instantiate(player);
            
            DontDestroyOnLoad(newPlayer);
            newPlayer.transform.position = transform.position;
        }
        else
        {
            FindObjectOfType<PlayerController>().transform.position = transform.position;
        }
        
        
    }
}
