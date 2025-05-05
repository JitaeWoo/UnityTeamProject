using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool _isDied;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    public void AttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }

    public void SkillAnimation()
    {
        _animator.SetTrigger("Dash");
    }
    public void DyingAnimation()
    {
        _animator.SetTrigger("Die");
    }
}
