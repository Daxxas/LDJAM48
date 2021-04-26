using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoader
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMain()
    {
        GameObject[] ddolObj = Resources.LoadAll<GameObject>("Main");

        foreach (var gameObject in ddolObj)
        {
            var obj = GameObject.Instantiate(gameObject);
            GameObject.DontDestroyOnLoad(obj);
        }
        
    }
}
