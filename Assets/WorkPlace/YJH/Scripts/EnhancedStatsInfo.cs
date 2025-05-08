using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnhancedStatsInfo
{
    public static Dictionary<string, float> EnhancedStatsDic = new Dictionary<string, float>();

    static EnhancedStatsInfo()
    {
        EnhancedStatsDic["HP"] = 0;
        EnhancedStatsDic["Speed"] = 0;
        EnhancedStatsDic["Damage"] = 0;
        EnhancedStatsDic["InvincibleTime"] = 0;
        EnhancedStatsDic["ShotSpeed"] = 0;
        EnhancedStatsDic["FireRate"] = 0;
        EnhancedStatsDic["ProjectileNum"] = 0;
        EnhancedStatsDic["PierceNum"] = 0;
        EnhancedStatsDic["Healed"] = 0;
    }

    static public void Init()
    {
        EnhancedStatsDic["HP"] = 0;
        EnhancedStatsDic["Speed"] = 0;
        EnhancedStatsDic["Damage"] = 0;
        EnhancedStatsDic["InvincibleTime"] = 0;
        EnhancedStatsDic["ShotSpeed"] = 0;
        EnhancedStatsDic["FireRate"] = 0;
        EnhancedStatsDic["ProjectileNum"] = 0;
        EnhancedStatsDic["PierceNum"] = 0;
        EnhancedStatsDic["Healed"] = 0;
    }
}
