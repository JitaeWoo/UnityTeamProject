using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetPortrait2 : MonoBehaviour
{
    private Button thisButton;
    public Button button1;
    public Button acceptbutton;
    public TMP_InputField nameinput;
    public void OnSetPortraitImage2()
    {
        // ���� ���� �ε�� �� ����� ��������Ʈ �̸� ����
        SceneData.spriteToSet = "Player_Portrait_02";
        thisButton = GetComponent<Button>();
        button1.interactable = true;
        thisButton.interactable = false;
        acceptbutton.interactable = true;
        nameinput.interactable = true;
    }
}
