using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class PlayerStats
{
    private int _maxHp;
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; OnMaxHpChanged?.Invoke(); } }
    private int _curHp; 
    public int CurHp { get { return _curHp; } set { _curHp = value; OnCurHpChanged?.Invoke(); } }
    public float Speed;
    public int Damage;
    public float InvincibleTime;
    // ShotSize ���� ��� ����
    public float ShotSize;
    public float ShotSpeed;
    public float FireRate;
    public int ProjectileNum;
    public int PierceNum;
    // źȯ ����. ���� ���Ҽ��� ����
    // public bool IsBulletHoming; 
    public event Action OnMaxHpChanged;
    public event Action OnCurHpChanged;
}
