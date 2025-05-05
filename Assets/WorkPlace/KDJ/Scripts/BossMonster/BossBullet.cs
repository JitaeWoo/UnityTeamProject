using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletRigid;

    private GameObject _boss;
    public Stack<GameObject> returnPool;

    private void Awake()
    {
        _boss = GameObject.FindGameObjectWithTag("Enemy");
    }
    void OnEnable()
    {
        StartCoroutine(ReturnBullet(3f));
    }

    private void FixedUpdate()
    {
        if (_boss.GetComponent<BossController>().CurHp <= 0)
            RemoveBullet();
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
        if(_boss.gameObject.name == "Boss" && _boss.GetComponent<BossController>().CurHp > 0)
            transform.SetParent(_boss.transform);
        gameObject.SetActive(false);
        returnPool.Push(gameObject);
    }
}

