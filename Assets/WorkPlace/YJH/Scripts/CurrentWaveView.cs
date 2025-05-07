using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentWaveView : MonoBehaviour
{
    public TMP_Text currentWaveText;

    public void Start()
    {
        WaveIndexImport();
    }

    public void Update()
    {
        WaveIndexImport();
    }

    public void WaveIndexImport()
    {
        currentWaveText.text = $"{Manager.Stage.Stage.currentWaveIndex + 1}";
    }
}
