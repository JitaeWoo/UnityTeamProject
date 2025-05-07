using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.EventSystems;

public class Dash : Skill
{
    [SerializeField] private float _dashCooldown = 2f;
    [SerializeField] private float _delay = 0f;

    private PlayerController _playerController;

    private void Awake()
    {
        CoolDown = _dashCooldown;
        SkillMotionDelay = _delay;
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerController = GetComponent<PlayerController>();
    }

    protected override void ActivateSkill()
    {
        // AddForce Dash
        // GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);

        // AddForce Dash at moveDirection
        GetComponent<Rigidbody>().AddForce(_playerController.GetMoveDirection() * 30f, ForceMode.Impulse);
    }
}
