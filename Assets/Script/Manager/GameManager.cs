using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action OnStartGame;
    public event Action OnGameOver;
    public event Action<bool> OnPaused;

    public void PauseGame(bool isPause)
    {
        OnPaused?.Invoke(isPause);
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
        // TODO : Stage�� ���ߵǸ� �ش� �̸����� �ٲ� ��
        Manager.Player.InitStats();
        EnhancedStatsInfo.Init();
        SceneChange("Stage1");
    }

    public void GameOver()
    {
        // TODO : GameOver UI ����
        // TODO : Game ���� ��Ȳ ��� ����?
        OnGameOver?.Invoke();
    }

    public void SceneChange(string sceneName)
    {
        if (sceneName != "TitleScene")
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Manager.Player.CreatePlayer(Vector3.zero);
        Camera.main.transform.AddComponent<CameraMove>();
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.LoadScene("UI_Scene", LoadSceneMode.Additive);
    }
}
