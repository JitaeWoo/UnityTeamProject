using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerStats
{
    public int MaxHp { get { return MaxHp; } set { MaxHp = value; OnMaxHpChanged?.Invoke(); } }
    public int CurHp { get { return CurHp; } set { MaxHp = value; OnCurHpChanged?.Invoke(); } }
    public float Speed;
    public int Damage;
    public float ShotSize;
    public int ProjectileNum;
    public event Action OnMaxHpChanged;
    public event Action OnCurHpChanged;
}
