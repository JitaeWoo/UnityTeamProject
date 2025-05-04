using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamagable
{
    enum AttackPattern
    {
        BulletShot, DashAttack
    }

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _attackPattern;
    [SerializeField] private GameObject _attackPatternTranslucent;
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;

    private Stack<GameObject> _bulletPool;
    private Coroutine _bossRoutine;
    private GameObject _player;
    private Action _bossPattern;
    private WaitForSeconds _Sleep;
    private bool _isDashEnd = false;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_bossRoutine == null)
        {
            PatternSelect((AttackPattern)UnityEngine.Random.Range(1, 2));
            _bossRoutine = StartCoroutine(PatternRoutine());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<IDamagable>()?.TakeHit(_damage);
        }
    }


    void BulletShot()
    {

    }

    void DashAttack()
    {
        Coroutine dash = StartCoroutine(BossDash());

        dash = null;
    }

    void Init()
    {
        _player = Manager.Player.Player;

        _bulletPool = new Stack<GameObject>(200);

        for (int i = 0; i < 200; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.GetComponent<BossBullet>().returnPool = _bulletPool;
            bullet.SetActive(false);
            _bulletPool.Push(bullet);
        }
    }

    void PatternSelect(AttackPattern pattern)
    {
        switch (pattern)
        {
            case AttackPattern.BulletShot:
                _bossPattern += BulletShot;
                break;
            case AttackPattern.DashAttack:
                _bossPattern += DashAttack;
                break;
        }
    }

    IEnumerator PatternRoutine()
    {
        _bossPattern.Invoke();
        yield return new WaitForSeconds(5f);
        _bossPattern -= BulletShot;
        _bossPattern -= DashAttack;
        _bossRoutine = null;
    }

    IEnumerator BossDash()
    {
        // 대쉬 방향 화살표 생성
        GameObject patternInstance1 = Instantiate(_attackPattern);
        GameObject patternInstance2 = Instantiate(_attackPatternTranslucent);

        _Sleep = new WaitForSeconds(0.02f);
        float count = 0.02f;

        patternInstance2.transform.position = new Vector3(_boss.transform.position.x, 0.1f, _boss.transform.position.z);
        patternInstance2.transform.Translate(_boss.transform.up * 6f);
        patternInstance1.transform.position = new Vector3(_boss.transform.position.x, 0.1f, _boss.transform.position.z);
        patternInstance1.transform.Translate(_boss.transform.up * 6f);
        patternInstance1.transform.localScale = new Vector3(1, 0, 1);
        for (int i = 0; i < 50; i++)
        {
            yield return _Sleep;
            patternInstance1.transform.localScale = new Vector3(1, 0 + count, 1);
            count += 0.02f;
        }

        Destroy(patternInstance1);
        Destroy(patternInstance2);

        _Sleep = new WaitForSeconds(0.5f);

        
        for (int i = 0; i < 3; i++)
        {
            // 대쉬 후 사방에 탄 발사
            int randomNum = 0;
            int maxAngle = 0;
            int testcount = 0;
            // 누적 각도가 360이 넘으면 생성중지
            while (maxAngle < 360)
            {
                testcount++;
                GameObject instance = _bulletPool.Pop();
                // 일정 랜덤 각도(예시 5~10)씩 틀어지며 탄 생성
                randomNum = UnityEngine.Random.Range(5, 10);
                // randomNum = 10;
                maxAngle += randomNum;
                instance.SetActive(true);
                instance.transform.position = _boss.transform.position;
                instance.transform.rotation = _boss.transform.rotation;
                instance.transform.Rotate(Vector3.up, maxAngle);
                // 생성된 탄환 발사
                instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * 15f, ForceMode.Impulse);
            }

            yield return _Sleep;
        }
    }

    public void TakeHit(int attackPoint)
    {
        _hp -= attackPoint;

        if (_hp < 0)
            Destroy(gameObject);
    }
}
