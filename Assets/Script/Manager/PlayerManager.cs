using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] private PlayerStats _stats = new PlayerStats();
    public PlayerStats Stats => _stats;
    private GameObject _player;
    public GameObject Player => _player;
    public event Action OnDied;

    private void Awake()
    {
        InitStats();
        _stats.OnCurHpChanged += () =>
        {
            if (_stats.CurHp <= 0)
            {
                Die();
            }
        };
    }

    private void Die()
    {
        OnDied?.Invoke();
    }

    public void InitStats()
    {
        _stats.MaxHp = 100;
        _stats.CurHp = _stats.MaxHp;
        _stats.Speed = 5f;
        _stats.Damage = 10;
        _stats.ShotSize = 1;
        _stats.ProjectileNum = 1;
        _stats.InvincibleTime = 0.1f;
        _stats.ShotSpeed = 10;
        _stats.FireRate = 1;
        _stats.PierceNum = 0;
    }

    public void CreatePlayer(Vector3 position)
    {
        _player = Instantiate(_playerPrefab, position, Quaternion.identity);
    }
}
