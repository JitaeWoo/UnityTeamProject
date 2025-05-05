using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Destroy(GameObject.FindWithTag("Blocker"));
        Destroy(transform.parent.gameObject);
    }
}
