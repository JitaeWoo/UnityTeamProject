using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancementPopupSpawner : MonoBehaviour
{
    public GameObject EnhancementPopup;
    private string targetCanvasName = "UI_Canvas";

    public void SpawnEnhancementPopup()
    {
        GameObject canvasObj = GameObject.Find(targetCanvasName);
        Instantiate(EnhancementPopup, canvasObj.transform);
    }
}
