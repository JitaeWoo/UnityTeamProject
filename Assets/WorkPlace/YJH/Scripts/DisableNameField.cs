using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableNameField : MonoBehaviour
{
    public TMP_InputField nameInput;
    public void Awake()
    {
        nameInput.interactable = false;
    }
}
