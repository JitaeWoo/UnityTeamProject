using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AcceptButton : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button acceptButton;
    public GameObject[] targetsToDestroy;
    public GameObject SkillSelectUI;
    public Transform SkillSelectLocation;

    public void Awake()
    {
        acceptButton.interactable = false;
    }

    public void OnConfirm()
    {
        PlayerNameControll.playerName = nameInputField.text;
        foreach (GameObject obj in targetsToDestroy)
        {
            Destroy(obj);
        }
        Instantiate(SkillSelectUI, SkillSelectLocation);
    }
}
