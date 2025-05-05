using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestCode_BossHealthCheck : MonoBehaviour
{
    [SerializeField] TMP_Text TMP_Text;
    [SerializeField] private GameObject _boss;
    private BossController _bossController;

    private void Awake()
    {
        _bossController = _boss.GetComponent<BossController>();
    }


    private void FixedUpdate()
    {
        SetHealthTest();
    }

    void SetHealthTest()
    {
        TMP_Text.text = $"<color=#FF0000>{_bossController.MaxHp} / {_bossController.CurHp}";
    }
}
