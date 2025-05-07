using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePopup : MonoBehaviour
{
    [SerializeField] private GameObject Popup;
    public GameObject blocker;
    private string canvas = "UI_Canvas";
    
    private void ShowPopup()
    {
        GameObject spawnLocation = GameObject.Find(canvas);
        Instantiate(blocker, spawnLocation.transform); // UI 씬 기준으로 블로커 생성
        Instantiate(Popup, spawnLocation.transform); // 마찬가지로 팝업 생성
    }
}
