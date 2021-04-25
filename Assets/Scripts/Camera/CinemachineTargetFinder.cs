using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineTargetFinder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TrySetFollow();
    }

    public void TrySetFollow()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = FindObjectOfType<PlayerController>().transform;

    }
} 
