using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletRigid;

    public Stack<GameObject> returnPool;

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

        if (other.gameObject.layer == 3)
        {
            other.GetComponentInParent<IDamagable>()?.TakeHit(10);
            StartCoroutine(ReturnBullet());
        }
    }

    public void RemoveBullet()
    {
        StartCoroutine(ReturnBullet());
    }

    IEnumerator ReturnBullet(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        _bulletRigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
        returnPool.Push(gameObject);
    }
}

