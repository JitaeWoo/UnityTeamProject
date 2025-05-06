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
        Instantiate(blocker, spawnLocation.transform); // UI �� �������� ���Ŀ ����
        Instantiate(Popup, spawnLocation.transform); // ���������� �˾� ����
    }
}
