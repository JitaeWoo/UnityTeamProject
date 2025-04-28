using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action OnStartGame;
    public event Action OnGameOver;

    public void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
