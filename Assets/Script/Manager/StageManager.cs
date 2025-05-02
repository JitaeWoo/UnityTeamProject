using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    public event Action OnStageClear;
    public event Action OnStageStart;
    public event Action OnWaveStart;
    public bool IsWaveClear;
    public string NextScene;

    public void StageStart()
    {
        OnStageStart?.Invoke();
    }

    public void StartWave()
    {
        OnWaveStart?.Invoke();
    }

    public void StageClear()
    {
        OnStageClear?.Invoke();
    }

}
