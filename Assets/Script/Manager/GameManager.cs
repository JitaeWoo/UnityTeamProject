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
        // TODO : Stage가 개발되면 해당 이름으로 바꿀 것
        Manager.Player.InitStats();
        SceneChange("Stage1");
    }

    public void GameOver()
    {
        // TODO : GameOver UI 생성
        // TODO : Game 진행 상황 모두 정지?
        OnGameOver?.Invoke();
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        if(sceneName != "Title")
        {
            // TODO : 올바른 위치에 플레이어를 생성하도록 수정
            Manager.Player.CreatePlayer(Vector3.zero);
            Camera.main.transform.AddComponent<CameraMove>();
        }
    }
}
