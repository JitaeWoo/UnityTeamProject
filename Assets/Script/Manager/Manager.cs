using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager 
{
    public static GameManager Game => GameManager.GetInctance();
    public static PlayerManager Player => PlayerManager.GetInctance();
    public static StageManager Stage => StageManager.GetInctance();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initailize()
    {
        GameManager.CreateInstance();
        PlayerManager.CreateInstance();
        StageManager.CreateInstance();
    }
}
