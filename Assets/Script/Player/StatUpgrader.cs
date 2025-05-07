using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrader
{
    public enum Stats
    {
        MaxHp, CurHp, Speed, Damage, InvincibleTime, ShotSpeed, FireRate, ProjectileNum, PierceNum, Size
    }

    public string GetStatUpgradeInfo(Stats stat)
    {
        switch (stat)
        {
            case Stats.MaxHp:
                return "최대 체력 10% 증가";
            case Stats.CurHp:
                return "최대 체력으로 회복";
            case Stats.Speed:
                return "스피드 20% 증가";
            case Stats.Damage:
                return "데미지 20% 증가";
            case Stats.InvincibleTime:
                return "무적 시간 20% 증가";
            case Stats.ShotSpeed:
                return "탄환 속도 20% 증가";
            case Stats.FireRate:
                return "공격 속도 20% 증가";
            case Stats.ProjectileNum:
                return "투사체 수 1 증가";
            case Stats.PierceNum:
                return "관통 수 1 증가";
            default:
                return "";
        }
    }


    public void UpgradeStat(Stats stat)
    {
        switch (stat)
        {
            case Stats.MaxHp:
                int beforeHp = Manager.Player.Stats.MaxHp;
                Manager.Player.Stats.MaxHp = (int)(Manager.Player.Stats.MaxHp * 1.1f);
                Manager.Player.Stats.CurHp += Manager.Player.Stats.MaxHp - beforeHp;
                EnhancedStatsInfo.EnhancedStatsDic["HP"] += 1;
                break;
            case Stats.CurHp:
                EnhancedStatsInfo.EnhancedStatsDic["Healed"] = Manager.Player.Stats.MaxHp - Manager.Player.Stats.CurHp;
                Manager.Player.Stats.CurHp = Manager.Player.Stats.MaxHp;
                break;
            case Stats.Speed:
                Manager.Player.Stats.Speed *= 1.2f;
                EnhancedStatsInfo.EnhancedStatsDic["Speed"] += 1;
                break;
            case Stats.Damage:
                Manager.Player.Stats.Damage = (int)(Manager.Player.Stats.Damage * 1.2f);
                EnhancedStatsInfo.EnhancedStatsDic["Damage"] += 1;
                break;
            case Stats.InvincibleTime:
                Manager.Player.Stats.InvincibleTime *= 1.2f;
                EnhancedStatsInfo.EnhancedStatsDic["InvincibleTime"] += 1;
                break;
            case Stats.ShotSpeed:
                Manager.Player.Stats.ShotSpeed *= 1.2f;
                EnhancedStatsInfo.EnhancedStatsDic["ShotSpeed"] += 1;
                break;
            case Stats.FireRate:
                Manager.Player.Stats.FireRate *= 0.8f;
                EnhancedStatsInfo.EnhancedStatsDic["FireRate"] += 1;
                break;
            case Stats.ProjectileNum:
                Manager.Player.Stats.ProjectileNum += 1;
                EnhancedStatsInfo.EnhancedStatsDic["ProjectileNum"] += 1;
                break;
            case Stats.PierceNum:
                Manager.Player.Stats.PierceNum += 1;
                EnhancedStatsInfo.EnhancedStatsDic["PierceNum"] += 1;
                break;
        }
    }
}
