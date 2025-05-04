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
        Instantiate(pauseMenu, parentTransform); // �Ͻ����� �޴� �˾� ����
    }
}