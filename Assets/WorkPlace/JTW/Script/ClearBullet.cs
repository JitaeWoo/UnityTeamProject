using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBullet : Skill
{
    [SerializeField] private float _skillCooldown = 1f;
    private Collider[] bullets = new Collider[200];

    // 플레이어의 정면을 기준으로 각도를 계산하기 때문에, 원하는 스킬 범위의 절반 각도가 필요합니다.
    private float _halfAngle = 45f;
    private LayerMask _layerMask;

    private void Awake()
    {
        CoolDown = _skillCooldown;
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

            Debug.Log(angle);

            if(angle <= _halfAngle)
            {
                // TODO : 추후에 EnemyBullet 만드시는 분과 협의하여 삭제 로직 재작성
                Destroy(bullets[i].gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 7f);
    }
}
