using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoader
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMain()
    {
        GameObject main = GameObject.Instantiate(Resources.Load("Main/LevelManager")) as GameObject;
        GameObject.DontDestroyOnLoad(main);
        
        GameObject player = GameObject.Instantiate(Resources.Load("Main/Player")) as GameObject;
        GameObject.DontDestroyOnLoad(player);

    }
}
