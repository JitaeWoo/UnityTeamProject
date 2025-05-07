using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOnDead : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPopup; // ���� ���� �˾� ������Ʈ
    public Transform popuptransform;
    public GameObject blocker;
    private string canvas = "UI_Canvas";

    private void Start()
    {
        // PlayerManager�� OnDied �̺�Ʈ ����
        Manager.Player.OnDied += ShowGameOverPopup;
    }

    // ���� ���� �˾��� ���� �޼���
    private void ShowGameOverPopup()
    {
        Time.timeScale = 0f;
        GameObject spawnLocation = GameObject.Find(canvas);
        Instantiate(blocker, spawnLocation.transform); // UI �� �������� ���Ŀ ����
        // ���� ���� �˾� Ȱ��ȭ
        Instantiate(gameOverPopup, popuptransform);
    }
}
