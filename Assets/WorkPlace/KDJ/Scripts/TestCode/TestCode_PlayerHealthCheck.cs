using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestCode_PlayerHealthCheck : MonoBehaviour
{
    [SerializeField] TMP_Text TMP_Text;

    private void FixedUpdate()
    {
        SetHealthTest();
    }

    void SetHealthTest()
    {
        TMP_Text.text = $"<color=#2db400>{Manager.Player.Stats.MaxHp} / {Manager.Player.Stats.CurHp}";
    }
}
