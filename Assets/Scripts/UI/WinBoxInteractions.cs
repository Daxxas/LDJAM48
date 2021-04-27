using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBoxInteractions : MonoBehaviour
{
    private IEnumerator Start()
    {
        if (LevelManager.Instance.currentBiomeIndex >= LevelManager.Instance.levelBiomeOrder.Count - 1)
        {
            yield return new WaitForSeconds(0.1f);
            Appear();
        }
    }

    public void Appear()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).localScale = Vector3.zero;
        LeanTween.scale(transform.GetChild(0).gameObject, new Vector3(1f, 1f, 1f), 0.5f);
    }

    public void Close()
    {
        
        LeanTween.scale(transform.GetChild(0).gameObject, new Vector3(0f, 0f, 0f), 0.5f);
    }
}
