using System;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsPlayer;
    public List<CharacterController> levelBosses;

    private int deadBosses = 0;

    public void AddConditionBoss(CharacterController boss)
    {
        levelBosses.Add(boss);
        boss.onDeath += UpdateAppear;

    }
    
    private void UpdateAppear()
    {
        deadBosses++;
        if (deadBosses >= levelBosses.Count)
        {
            Appear();
        }
    }
    
    private void Appear()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ExitTrigger(CharacterController other)
    {
        var levelManager = FindObjectOfType<LevelManager>();
        
        if (levelManager != null)
        {
            levelManager.SwitchToNextLevel();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
