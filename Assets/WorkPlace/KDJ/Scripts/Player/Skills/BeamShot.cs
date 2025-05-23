using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : Skill
{
    [SerializeField] private float _beamCooldown = 10f;
    [SerializeField] private float _delay = 0.65f;
    [SerializeField] private GameObject _beamPrefab;

    private PlayerController _player;

    private void Awake()
    {
        CoolDown = _beamCooldown;
        SkillMotionDelay = _delay;
        _playerAnimation = GetComponent<PlayerAnimation>();
        _beamPrefab = Resources.Load<GameObject>("Beam");
        _player = GetComponent<PlayerController>();
    }

    protected override void ActivateSkill()
    {
        GameObject instance = Instantiate(_beamPrefab);
        instance.transform.position = _player.GetMuzzleTransform().position;
        instance.transform.rotation = _player.GetMuzzleTransform().rotation;
        instance.transform.Rotate(Vector3.right, 90f);
        instance.transform.Translate(instance.transform.forward * -40);
    }
}
