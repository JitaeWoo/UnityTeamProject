using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SetPortrait1 : MonoBehaviour
{
    private Button thisButton;
    public Button button2;
    public void OnSetPortraitImage1()
    {
        // ���� ���� �ε�� �� ����� ��������Ʈ �̸� ����
        SceneData.spriteToSet = "Player_Portrait_01";
        thisButton = GetComponent<Button>();
        button2.interactable = true;
        thisButton.interactable = false;
    }
}
