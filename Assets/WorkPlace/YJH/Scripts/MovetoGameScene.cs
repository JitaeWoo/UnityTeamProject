using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovetoGameScene : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Button>().interactable = false;
    }
    public void GoToGameScene()
    {
        Manager.Game.StartGame();
    }
}
