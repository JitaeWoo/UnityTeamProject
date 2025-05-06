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
    public TMP_Text plusedHealedText;
    public TMP_Text plusedShotSpeedText;
    public TMP_Text plusedFireRateText;
    public TMP_Text plusedProjectileNumText;
    public TMP_Text plusedPierceNumText;

    private PlayerStats stats;

    void Start()
    {
        UpdateStatsUI();
    }

    void Update()
    {
        // ������ Update���� ����
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        plusedHPText.text = $"{EnhancedStatsInfo.EnhancedStatsDic["HP"]}";
        plusedSpeedText.text = $"{EnhancedStatsInfo.EnhancedStatsDic["Speed"]}%";
        plusedDamageText.text = $"{EnhancedStatsInfo.EnhancedStatsDic["Damage"]}%";
        plusedInvincibeTimeText.text = $"{EnhancedStatsInfo.EnhancedStatsDic["InvincibleTime"]}%";
        plusedShotSpeedText.text = $"{EnhancedStatsInfo.EnhancedStatsDic["ShotSpeed"]}%";
        plusedFireRateText.text = $"{EnhancedStatsInfo.EnhancedStatsDic["FireRate"]}%";
        plusedProjectileNumText.text = $"{EnhancedStatsInfo.EnhancedStatsDic["ProjectileNum"]}";
        plusedPierceNumText.text = $"{EnhancedStatsInfo.EnhancedStatsDic["PierceNum"]}";
        plusedHealedText.text = $"{EnhancedStatsInfo.EnhancedStatsDic["Healed"]}";
    }
}