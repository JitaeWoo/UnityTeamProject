using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Skill
{
    [SerializeField] private float _SlashCooldown = 1f;
    private Collider[] _enemies = new Collider[40];

    private float _halfAngle = 90f;
    private LayerMask _layerMask;

    private void Awake()
    {
        CoolDown = _SlashCooldown;
        _layerMask = 1 << LayerMask.NameToLayer("Enemy");
    }

    protected override void ActivateSkill()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, 5f, _enemies, _layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            Vector3 playerPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 enemyPosition = new Vector3(_enemies[i].transform.position.x, 0, _enemies[i].transform.position.z);
            Vector3 directionToBullet = (enemyPosition - playerPosition).normalized;

            float angle = Vector3.Angle(transform.forward, directionToBullet);

            if (angle <= _halfAngle)
            {
                _enemies[i].GetComponent<IDamagable>()?.TakeHit(50);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
