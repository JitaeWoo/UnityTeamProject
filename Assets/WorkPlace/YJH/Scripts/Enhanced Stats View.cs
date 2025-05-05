using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnhancedStatsView : MonoBehaviour
{
    [Header("�ؽ�Ʈ UI ����")]
    public TMP_Text plusedHPText;
    public TMP_Text plusedSpeedText;
    public TMP_Text plusedDamageText;
    public TMP_Text plusedInvincibeTimeText;
    public TMP_Text plusedShotSizeText;
    public TMP_Text plusedShotSpeedText;
    public TMP_Text plusedFireRateText;
    public TMP_Text plusedProjectileNumText;
    public TMP_Text plusedPierceNumText;

    private PlayerStats stats;

    void Start()
    {
        // manager���� player�� stats ��������
        stats = Manager.Player.Stats;
        // �ʱ� ǥ��
        UpdateStatsUI();
    }

    void Update()
    {
        // ������ Update���� ����
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        plusedHPText.text = $"{stats.MaxHp - 100}";
        plusedSpeedText.text = $"{stats.Speed - 5f}";
        plusedDamageText.text = $"{stats.Damage -10}";
        plusedInvincibeTimeText.text = $"{stats.InvincibleTime - 0.1f}";
        plusedShotSizeText.text = $"{stats.ShotSize - 1}";
        plusedShotSpeedText.text = $"{stats.ShotSpeed - 10}";
        plusedFireRateText.text = $"{stats.FireRate - 1}";
        plusedProjectileNumText.text = $"{stats.ProjectileNum - 1}";
        plusedPierceNumText.text = $"{stats.PierceNum - 0}";
    }
}