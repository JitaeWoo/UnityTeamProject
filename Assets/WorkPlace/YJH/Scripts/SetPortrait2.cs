using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPortrait2 : MonoBehaviour
{
    private Button thisButton;
    public Button button1;
    public void OnSetPortraitImage2()
    {
        // 게임 씬이 로드될 때 사용할 스프라이트 이름 설정
        SceneData.spriteToSet = "Player_Portrait_02";
        thisButton = GetComponent<Button>();
        button1.interactable = true;
        thisButton.interactable = false;
    }
}
