using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOnDead : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPopup; // ���� ���� �˾� ������Ʈ
    public Transform popuptransform;

    private void Start()
    {
        // PlayerManager�� OnDied �̺�Ʈ ����
        Manager.Player.OnDied += ShowGameOverPopup;
    }

    // ���� ���� �˾��� ���� �޼���
    private void ShowGameOverPopup()
    {
        // ���� ���� �˾� Ȱ��ȭ
        Instantiate(gameOverPopup, popuptransform);
    }
}
