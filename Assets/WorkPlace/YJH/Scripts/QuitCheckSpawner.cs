using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitCheckSpawner : MonoBehaviour
{
    public GameObject QuitPopup;
    public GameObject Blocker;
    private string targetCanvasName = "Canvas";
    public void SpawnQuitCheck()
    {
        GameObject canvasObj = GameObject.Find(targetCanvasName);
        GameObject spawnedBlocker = Instantiate(Blocker, canvasObj.transform); // ���Ŀ ����
        Instantiate(QuitPopup, canvasObj.transform);
    }
}
