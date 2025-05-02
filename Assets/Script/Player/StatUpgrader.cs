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
                return "�ִ� ü�� 10 ����";
            case Stats.CurHp:
                return "�ִ� ü������ ȸ��";
            case Stats.Speed:
                return "���ǵ� 10% ����";
            case Stats.Damage:
                return "������ 10% ����";
            case Stats.InvincibleTime:
                return "���� �ð� 20% ����";
            case Stats.ShotSpeed:
                return "źȯ �ӵ� 10% ����";
            case Stats.FireRate:
                return "���� �ӵ� 10% ����";
            case Stats.ProjectileNum:
                return "����ü �� 1 ����";
            case Stats.PierceNum:
                return "���� �� 1 ����";
            default:
                return "";
        }
    }


    public void UpgradeStat(Stats stat)
    {
        switch (stat)
        {
            case Stats.MaxHp:
                Manager.Player.Stats.MaxHp = (int)(Manager.Player.Stats.MaxHp * 1.1f);
                break;
            case Stats.CurHp:
                Manager.Player.Stats.CurHp = Manager.Player.Stats.MaxHp;
                break;
            case Stats.Speed:
                Manager.Player.Stats.Speed *= 1.2f;
                break;
            case Stats.Damage:
                Manager.Player.Stats.Damage *= (int)(Manager.Player.Stats.Damage * 1.2f);
                break;
            case Stats.InvincibleTime:
                Manager.Player.Stats.InvincibleTime *= 1.2f;
                break;
            case Stats.ShotSpeed:
                Manager.Player.Stats.ShotSpeed *= 1.2f;
                break;
            case Stats.FireRate:
                Manager.Player.Stats.FireRate *= 0.8f;
                break;
            case Stats.ProjectileNum:
                Manager.Player.Stats.ProjectileNum += 1;
                break;
            case Stats.PierceNum:
                Manager.Player.Stats.PierceNum += 1;
                break;
        }
    }
}
