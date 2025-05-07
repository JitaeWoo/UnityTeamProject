using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentStageView : MonoBehaviour
{
    public TMP_Text currentStageText;
    // Update is called once per frame
    public void Update()
    {
        string stageNum = (SceneManager.GetActiveScene().name).Replace("Stage","");
        currentStageText.text = stageNum;
    }
}
