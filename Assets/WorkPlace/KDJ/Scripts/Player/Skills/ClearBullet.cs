using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBullet : Skill
{
    [SerializeField] private float _skillCooldown = 5f;
    [SerializeField] private float _delay = 0.01f;
    private Collider[] bullets = new Collider[200];

    // �÷��̾��� ������ �������� ������ ����ϱ� ������, ���ϴ� ��ų ������ ���� ������ �ʿ��մϴ�.
    private float _halfAngle = 45f;
    private LayerMask _layerMask;

    private void Awake()
    {
        CoolDown = _skillCooldown;
        SkillMotionDelay = _delay;
        _playerAnimation = GetComponent<PlayerAnimation>();
        _layerMask = 1 << LayerMask.NameToLayer("EnemyBullet");
    }

    protected override void ActivateSkill()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, 7f, bullets, _layerMask);

        for(int i = 0; i < hitCount; i++)
        {
            Vector3 playerPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 bulletPosition = new Vector3(bullets[i].transform.position.x, 0, bullets[i].transform.position.z);
            Vector3 directionToBullet = (bulletPosition - playerPosition).normalized;

            float angle = Vector3.Angle(transform.forward, directionToBullet);

            if(angle <= _halfAngle)
            {
                // TODO : ���Ŀ� EnemyBullet ����ô� �а� �����Ͽ� ���� ���� ���ۼ�
                if (bullets[i].GetComponent<MonsterProjectileScript>() != null)
                {
                    bullets[i].GetComponent<MonsterProjectileScript>().Deactivate();
                }
                else if (bullets[i].GetComponent<BossBullet>() != null)
                {
                    bullets[i].GetComponent<BossBullet>().RemoveBullet();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 7f);
    }
}
