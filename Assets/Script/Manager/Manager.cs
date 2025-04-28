using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager 
{
    public GameManager Game => GameManager.GetInctance();
    public PlayerManager Player => PlayerManager.GetInctance();
    public StageManager Stage => StageManager.GetInctance();
    public UIManager UI => UIManager.GetInctance();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initailize()
    {
        GameManager.CreateInstance();
        PlayerManager.CreateInstance();
        StageManager.CreateInstance();
        UIManager.CreateInstance();
    }
}
