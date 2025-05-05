using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGameSceneButton : MonoBehaviour
{
    public Button GameSceneButton;

    public void ActivateGameSceneButton()
    {
        GameSceneButton.interactable = true;
    }
}
