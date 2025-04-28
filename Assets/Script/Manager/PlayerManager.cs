using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{ 
    private PlayerStats _stats;
    public PlayerStats Stats => _stats;

    private void Awake()
    {
        InitStats();
    }

    public void InitStats()
    {
        _stats.MaxHp = 100;
        _stats.CurHp = _stats.MaxHp;
        _stats.Speed = 5f;
        _stats.Damage = 10;
        _stats.ShotSize = 1;
        _stats.ProjectileNum = 1;
    }
}
