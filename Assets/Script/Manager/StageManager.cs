using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public event Action OnStageClear;
    public event Action OnStageStart;
    public bool IsWaveClear;

    public void StartWave()
    {

    }
}
