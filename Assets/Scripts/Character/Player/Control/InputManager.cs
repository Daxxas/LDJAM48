using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInputs playerInputs;

    private void Awake()
    {
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs.Gameplay.Movement.Enable();
        playerInputs.Gameplay.Dash.Enable();
        playerInputs.Gameplay.Attack.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Gameplay.Movement.Disable();
        playerInputs.Gameplay.Dash.Disable();
        playerInputs.Gameplay.Attack.Disable();
    }
    
    
}
