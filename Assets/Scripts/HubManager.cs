using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var player = FindObjectOfType<PlayerController>(); 
        player.transform.position = transform.position;
        player.Heal(player.MaxHealth);
        player.IsDead = false;

    }
}
