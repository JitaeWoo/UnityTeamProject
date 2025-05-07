using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class MoveWaveNaviPoint : MonoBehaviour
{
    public GameObject waveNaviPoint;
    private int currentWave;

    public void Update()
    {
        MovePoint();
    }

    public void MovePoint()
    {
        currentWave = Manager.Stage.Stage.currentWaveIndex + 1;
        RectTransform rect = waveNaviPoint.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;

        switch (currentWave)
        {
            case 2:
                pos.x = (float)-24.3;
                break;
            case 3:
                pos.x = 24.3f;
                break;
            case 4:
                pos.x = 75;
                break;
        }

        rect.anchoredPosition = pos;
    }
}
