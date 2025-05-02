using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameDisplay : MonoBehaviour
{
    public TMP_Text nameText;

    void Start()
    {
        nameText.text = PlayerNameControll.playerName;
    }
}
