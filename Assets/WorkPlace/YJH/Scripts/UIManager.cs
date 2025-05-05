using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // 게임 씬 시작 시 UI 씬을 Additive 방식으로 로드
    void Start()
    {
        LoadUI();
    }

    void LoadUI()
    {
        SceneManager.LoadScene("UI_Scene", LoadSceneMode.Additive);
    }

    // 게임 종료 시 UI 씬을 비활성화
    public void UnloadUI()
    {
        // UI 씬을 언로드하여 게임 씬 위에서 UI를 제거
        SceneManager.UnloadSceneAsync("UI_Scene");
    }
}