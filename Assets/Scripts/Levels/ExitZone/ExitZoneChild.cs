using System;
using UnityEngine;


public class ExitZoneChild : MonoBehaviour, Interactable
{
    public void Interact(CharacterController characterController)
    {
        Debug.Log("EXIT INTERACT");
        
        transform.GetComponentInParent<ExitZone>().ExitTrigger(characterController);
    }
}
