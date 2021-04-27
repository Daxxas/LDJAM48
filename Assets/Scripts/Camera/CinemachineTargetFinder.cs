using System;
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

    private CinemachineVirtualCamera vcam;

    public void TrySetFollow()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = FindObjectOfType<PlayerController>()?.transform;
    }

    private void Update()
    {
        vcam.ForceCameraPosition(new Vector3(0, transform.position.y, transform.position.z), Quaternion.identity);
    }
} 
