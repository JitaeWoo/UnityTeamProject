using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool _isDied;
    private bool _isAttacking;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    public void AttackAnimation()
    {
        _isAttacking = true;
        _animator.SetBool("IsAttacking", _isAttacking);
    }

    public void SkillAnimation()
    {
        _animator.SetTrigger("Skill");
    }

    public void DyingAnimation()
    {
        _isDied = true;
        _animator.SetBool("IsDied", _isDied);
    }
    
    public void StopAttack()
    {
        _isAttacking = false;
        _animator.SetBool("IsAttacking", _isAttacking);
    }
}
