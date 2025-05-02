using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider; // Slider 사용
    [SerializeField] private TMP_Text _hpText; // 현재/최대 체력 텍스트 UI
    private PlayerStats _playerStats;

    private void Start()
    {
        // PlayerManager 인스턴스에서 PlayerStats 참조 가져오기
        _playerStats = Manager.Player.Stats;

        // 초기화
        _hpSlider.maxValue = _playerStats.MaxHp;
        _hpSlider.value = _playerStats.CurHp;

        // 이벤트 구독
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
        // 최대 체력 증가만 처리 (현재 체력은 변경하지 않음)
    }

    private void UpdateHPText()
    {
        // 현재 체력/최대 체력 형식으로 텍스트 업데이트
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