using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action OnStartGame;
    public event Action OnGameOver;

    public void StartGame()
    {
        OnStartGame?.Invoke();
        SceneChange("Stage1");
        Manager.Player.InitStats();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        if(sceneName != "Title")
        {
            // TODO : �ùٸ� ��ġ�� �÷��̾ �����ϵ��� ����
            Manager.Player.CreatePlayer(Vector3.zero);
        }
    }
}
