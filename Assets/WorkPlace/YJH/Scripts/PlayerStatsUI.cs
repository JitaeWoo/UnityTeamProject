using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    [Header("텍스트 UI 연결")]
    public TMP_Text hpText;
    public TMP_Text speedText;
    public TMP_Text damageText;
    public TMP_Text invincibeTimeText;
    public TMP_Text shotSizeText;
    public TMP_Text shotSpeedText;
    public TMP_Text fireRateText;
    public TMP_Text projectileNumText;
    public TMP_Text pierceNumText;

    private PlayerStats stats;

    void Start()
    {
        // manager에서 player의 stats 가져오기
        stats = Manager.Player.Stats;

        // 초기 표시
        UpdateStatsUI();
    }

    void Update()
    {
        // 스탯을 Update마다 갱신
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        hpText.text = $"{stats.MaxHp} / {stats.CurHp}";
        speedText.text = $"{stats.Speed}";
        damageText.text = $"{stats.Damage}";
        invincibeTimeText.text = $"{stats.InvincibleTime}";
        shotSizeText.text = $"{stats.ShotSize}";
        shotSpeedText.text = $"{stats.ShotSpeed}";
        fireRateText.text = $"{stats.FireRate}";
        projectileNumText.text = $"{stats.ProjectileNum}";
        pierceNumText.text = $"{stats.PierceNum}";
    }
}
