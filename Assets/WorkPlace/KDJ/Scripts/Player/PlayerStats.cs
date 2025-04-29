using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private int _maxHp;
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; OnMaxHpChanged?.Invoke(); } }
    private int _curHp; 
    public int CurHp { get { return _curHp; } set { _curHp = value; OnCurHpChanged?.Invoke(); } }
    public float Speed;
    public int Damage;
    public float ShotSize;
    public float ShotSpeed;
    public float FireRate;
    public int ProjectileNum;
    public event Action OnMaxHpChanged;
    public event Action OnCurHpChanged;
}
