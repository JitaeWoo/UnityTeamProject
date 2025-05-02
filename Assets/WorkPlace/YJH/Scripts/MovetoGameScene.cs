using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovetoGameScene : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("UI_Scene");  // æ¿ ¿Ãµø
    }
}
