using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletRigid;

    public Stack<GameObject> returnPool;
    public int PierceNum;

    void OnEnable()
    {
        StartCoroutine(ReturnBullet(3f));
    }

    void OnTriggerEnter(Collider other)
    {
        // 벽에 충돌시 제거
        if (other.gameObject.layer == 6)
        {
            StartCoroutine(ReturnBullet());
        }

        // 몬스터에 충돌 시 관통 기능
        if (other.gameObject.layer == 9)
        {
            other.GetComponentInParent<IDamagable>()?.TakeHit(Manager.Player.Stats.Damage);

            if (PierceNum > 0)
            {
                PierceNum--;
            }
            else
            {
                StartCoroutine(ReturnBullet());
            }
        }
    }

    IEnumerator ReturnBullet(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        _bulletRigid.velocity = Vector3.zero;
        if (Manager.Player.Stats.CurHp > 0)
            transform.SetParent(Manager.Player.Player.gameObject.transform);
        gameObject.SetActive(false);
        returnPool.Push(gameObject);
    }
}
