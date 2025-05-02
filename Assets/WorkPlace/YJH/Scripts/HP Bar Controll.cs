using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider; // Slider ���
    [SerializeField] private TMP_Text _hpText; // ����/�ִ� ü�� �ؽ�Ʈ UI
    private PlayerStats _playerStats;

    private void Start()
    {
        // PlayerManager �ν��Ͻ����� PlayerStats ���� ��������
        _playerStats = Manager.Player.Stats;

        // �ʱ�ȭ
        _hpSlider.maxValue = _playerStats.MaxHp;
        _hpSlider.value = _playerStats.CurHp;

        // �̺�Ʈ ����
        _playerStats.OnCurHpChanged += UpdateHPBar;
        _playerStats.OnMaxHpChanged += UpdateMaxHP;
        _playerStats.OnCurHpChanged += UpdateHPText;
        _playerStats.OnMaxHpChanged += UpdateHPText;

        UpdateHPText();
    }

    private void UpdateHPBar()
    {
        _hpSlider.value = _playerStats.CurHp;
    }

    private void UpdateMaxHP()
    {
        _hpSlider.maxValue = _playerStats.MaxHp;
        // �ִ� ü�� ������ ó�� (���� ü���� �������� ����)
    }

    private void UpdateHPText()
    {
        // ���� ü��/�ִ� ü�� �������� �ؽ�Ʈ ������Ʈ
         _hpText.text = $"{_playerStats.CurHp}  /  {_playerStats.MaxHp}";
    }

    private void OnDestroy()
    {
        if (_playerStats != null)
        {
            _playerStats.OnCurHpChanged -= UpdateHPBar;
            _playerStats.OnMaxHpChanged -= UpdateMaxHP;
        }
    }
}