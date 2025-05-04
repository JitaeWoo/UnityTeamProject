using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuSpawner : MonoBehaviour
{
    public GameObject blocker; // 블로커 생성
    public GameObject pauseMenu; // 생성할 프리팹
    public Transform parentTransform; // 생성될 부모

    public void SpawnPauseMenu()
    {
        Time.timeScale = 0f; // 일시정지 기능을 함. UI 제외한 물리 동작 등은 멈춤.
        GameObject spawnedBlocker = Instantiate(blocker, parentTransform); // 블로커 생성
        Instantiate(pauseMenu, parentTransform); // 일시정지 메뉴 팝업 생성
    }
}