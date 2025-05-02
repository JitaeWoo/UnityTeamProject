using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action OnStartGame;
    public event Action OnGameOver;

    public void StartGame()
    {
        OnStartGame?.Invoke();
        // TODO : Stage�� ���ߵǸ� �ش� �̸����� �ٲ� ��
        Manager.Player.InitStats();
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
        SceneManager.LoadScene(sceneName);
        if(sceneName != "Title")
        {
            // TODO : �ùٸ� ��ġ�� �÷��̾ �����ϵ��� ����
            Manager.Player.CreatePlayer(Vector3.zero);
            Camera.main.transform.AddComponent<CameraMove>();
        }
    }
}
