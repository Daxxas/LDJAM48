using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Start()
    {
        var newPlayer = Instantiate(player);
        
        DontDestroyOnLoad(newPlayer);
        
        newPlayer.transform.position = transform.position;
    }
}
