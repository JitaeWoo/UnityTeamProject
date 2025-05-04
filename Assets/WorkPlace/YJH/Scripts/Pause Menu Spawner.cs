using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuSpawner : MonoBehaviour
{
    public GameObject blocker; // ���Ŀ ����
    public GameObject pauseMenu; // ������ ������
    public Transform parentTransform; // ������ �θ�

    public void SpawnPauseMenu()
    {
        Time.timeScale = 0f; // �Ͻ����� ����� ��. UI ������ ���� ���� ���� ����.
        GameObject spawnedBlocker = Instantiate(blocker, parentTransform); // ���Ŀ ����
        RectTransform rt = spawnedBlocker.GetComponent<RectTransform>(); // ���Ŀ�� RECTTRANSFORM ������

        // ���Ŀ�� ��ü ȭ���� ������ �ʱ�ȭ
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = Vector2.zero;

        Instantiate(pauseMenu, parentTransform); // �Ͻ����� �޴� ����
    }
}