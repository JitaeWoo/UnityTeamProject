using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.EventSystems;

public class Dash : Skill
{
    [SerializeField] private float _dashCooldown = 1f;

    private void Awake()
    {
        CoolDown = _dashCooldown;
    }

    protected override void ActivateSkill()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
    }
}
