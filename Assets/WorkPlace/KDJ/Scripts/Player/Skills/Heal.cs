using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Skill
{
    [SerializeField] private float _healCooldown = 1f;

    private void Awake()
    {
        CoolDown = _healCooldown;
    }

    protected override void ActivateSkill()
    {
        Manager.Player.Stats.CurHp += 30;
        if(Manager.Player.Stats.CurHp > Manager.Player.Stats.MaxHp)
            Manager.Player.Stats.CurHp = Manager.Player.Stats.MaxHp;
    }
}
