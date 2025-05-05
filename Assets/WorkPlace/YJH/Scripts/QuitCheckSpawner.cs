using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitCheckSpawner : MonoBehaviour
{
    public GameObject QuitPopup;
    public GameObject Blocker;
    public Transform parentTransform;
    public void SpawnQuitCheck()
    {
        GameObject spawnedBlocker = Instantiate(Blocker, parentTransform); // 블로커 생성
        Instantiate(QuitPopup, parentTransform);
    }
}
