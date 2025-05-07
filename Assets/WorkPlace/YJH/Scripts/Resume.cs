using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    public GameObject targetToDestroy;

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Manager.Game.PauseGame(false);
        Destroy(GameObject.FindWithTag("Blocker"));
        Destroy(targetToDestroy);
    }
}
