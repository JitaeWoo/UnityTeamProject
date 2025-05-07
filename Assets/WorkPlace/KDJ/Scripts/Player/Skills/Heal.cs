using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Skill
{
    [SerializeField] private float _healCooldown = 10f;
    [SerializeField] private float _delay = 0.65f;

    private void Awake()
    {
        CoolDown = _healCooldown;
        SkillMotionDelay = _delay;
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    protected override void ActivateSkill()
    {
        Manager.Player.Stats.CurHp += 30;
        if(Manager.Player.Stats.CurHp > Manager.Player.Stats.MaxHp)
            Manager.Player.Stats.CurHp = Manager.Player.Stats.MaxHp;
    }
}
