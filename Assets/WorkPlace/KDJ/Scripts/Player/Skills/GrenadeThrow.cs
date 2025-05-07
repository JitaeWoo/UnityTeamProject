using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : Skill
{
    [SerializeField] private float _grenadeCooldown = 5f;
    [SerializeField] private GameObject _grenadePrefab;
    
    private PlayerController _player;

    private void Awake()
    {
        CoolDown = _grenadeCooldown;
        _grenadePrefab = Resources.Load<GameObject>("Grenade");
        _player = GetComponent<PlayerController>();
    }

    protected override void ActivateSkill()
    {
        GameObject instance = Instantiate(_grenadePrefab);
        instance.transform.position = _player.GetMuzzleTransform().position;
        instance.transform.rotation = _player.GetMuzzleTransform().rotation;
        instance.transform.Rotate(Vector3.right, -7f);
        instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * 25f, ForceMode.Impulse);
    }
}
