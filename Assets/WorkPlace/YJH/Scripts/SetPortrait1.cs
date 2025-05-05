using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetPortrait1 : MonoBehaviour
{
    private Button thisButton;
    public Button button2;
    public Button acceptbutton;
    public TMP_InputField nameinput;
    public void OnSetPortraitImage1()
    {
        // 게임 씬이 로드될 때 사용할 스프라이트 이름 설정
        SceneData.spriteToSet = "Player_Portrait_01";
        thisButton = GetComponent<Button>();
        button2.interactable = true;
        thisButton.interactable = false;
        acceptbutton.interactable = true;
        nameinput.interactable = true;
    }
}
