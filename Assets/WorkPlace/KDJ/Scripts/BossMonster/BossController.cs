using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamagable
{
    enum AttackPattern
    {
        BulletShot = 1, DashAttack
    }

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _attackPattern;
    [SerializeField] private GameObject _attackPatternTranslucent;
    [SerializeField] private Rigidbody _bossRigid;
    [SerializeField] private int _damage;

    private Stack<GameObject> _bulletPool;
    private Coroutine _bossRoutine;
    private Action _bossPattern;
    private WaitForSeconds _Sleep;
    private BossAnimation _bossAnimation;
    private int _patternNum = 1;
    private int _maxHp;
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    private int _curHp;
    public int CurHp { get { return _curHp; } set { _curHp = value; OnCurHpChanged?.Invoke(); } }
    private int _halfHp;

    public event Action OnCurHpChanged;
    public event Action OnDied;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_bossRoutine == null && Manager.Player.Player != null)
        {
            if (_curHp > _halfHp)
                PatternSelect((AttackPattern)_patternNum);
            else
            {
                PatternSelect((AttackPattern)1);
                PatternSelect((AttackPattern)2);
            }
            _bossRoutine = StartCoroutine(PatternRoutine());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<IDamagable>()?.TakeHit(_damage);
        }
    }

    void BulletShot()
    {
        if (_curHp > 0 && Manager.Player.Stats.CurHp > 0)
        {
            Coroutine shot = StartCoroutine(BossShot());
            shot = null;
            _patternNum = 2;
        }
    }

    void DashAttack()
    {
        if (_curHp > 0 && Manager.Player.Stats.CurHp > 0)
        {
            Coroutine dash = StartCoroutine(BossDash());
            dash = null;
            _patternNum = 1;
        }
    }

    void Init()
    {
        _bulletPool = new Stack<GameObject>(300);
        _maxHp = 1500;
        _curHp = _maxHp;
        _halfHp = _maxHp / 2;
        _bossAnimation = GetComponentInChildren<BossAnimation>();

        for (int i = 0; i < 300; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.transform.parent = this.transform;
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

    IEnumerator BossShot()
    {
        WaitForSeconds sleep = new WaitForSeconds(0.65f);

        // 보스가 플레이어를 바라봄
        Vector3 lookPos = new Vector3(Manager.Player.Player.transform.position.x, 0, Manager.Player.Player.transform.position.z);
        _boss.transform.LookAt(lookPos);
        _bossAnimation.AttackAnimation();
        yield return sleep;

        sleep = new WaitForSeconds(0.25f);

        if (CurHp > 0)
        {
            for (int i = 0; i < 10; i++)
            {
                // 탄 발사시에도 계속 바라봄
                lookPos = new Vector3(Manager.Player.Player.transform.position.x, 0, Manager.Player.Player.transform.position.z);
                _boss.transform.LookAt(lookPos);


                for (int j = 0; j < 7; j++)
                {
                    // 플레이어 방향으로 부채꼴 탄막 발사
                    float startAngle;
                    float angleGrid;

                    startAngle = -30f;
                    angleGrid = 10f;

                    GameObject instance = _bulletPool.Pop();
                    instance.transform.SetParent(null);
                    instance.SetActive(true);
                    instance.transform.position = new Vector3(_boss.transform.position.x, 0.4f, _boss.transform.position.z);
                    instance.transform.rotation = _boss.transform.rotation;
                    instance.transform.Rotate(0, startAngle + angleGrid * j, 0);
                    instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * 15f, ForceMode.Impulse);
                    instance.transform.Rotate(0, -(startAngle + angleGrid * j), 0);
                }
                yield return sleep;
            }
        }
    }

    IEnumerator BossDash()
    {

        // 보스가 플레이어를 바라봄
        Vector3 lookPos = new Vector3(Manager.Player.Player.transform.position.x, 0, Manager.Player.Player.transform.position.z);
        _boss.transform.LookAt(lookPos);

        // 대쉬 방향 화살표 생성
        GameObject patternInstance1 = Instantiate(_attackPattern);
        GameObject patternInstance2 = Instantiate(_attackPatternTranslucent);

        // 아래 반복문의 반복수와 맞춰 1~2초 사이로 만들어야함
        _Sleep = new WaitForSeconds(0.02f);
        float count = 0.02f;

        // 대쉬 준비 시간 시각적 표시
        patternInstance2.transform.position = new Vector3(_boss.transform.position.x, 0.1f, _boss.transform.position.z);
        patternInstance2.transform.rotation = _boss.transform.rotation;
        patternInstance2.transform.Rotate(Vector3.right, 90f);
        patternInstance2.transform.Translate(_boss.transform.up * 6f);
        patternInstance1.transform.position = new Vector3(_boss.transform.position.x, 0.1f, _boss.transform.position.z);
        patternInstance1.transform.rotation = _boss.transform.rotation;
        patternInstance1.transform.Rotate(Vector3.right, 90f);
        patternInstance1.transform.Translate(_boss.transform.up * 6f);
        patternInstance1.transform.localScale = new Vector3(1, 0, 1);


        for (int i = 0; i < 50; i++)
        {
            yield return _Sleep;
            patternInstance1.transform.localScale = new Vector3(1, 0 + count, 1);
            count += 0.02f;
        }

        // 화살표 삭제
        Destroy(patternInstance1);
        Destroy(patternInstance2);

        // 보스 대쉬
        // _Sleep = new WaitForSeconds(0.01f);
        // for (int i = 0; i < 50; i++)
        // {
        //     _boss.transform.Translate(_boss.transform.forward * 0.3f);
        //     yield return _Sleep;
        // }

        // 플레이어가 보스를 미는걸 막기위해 보스의 중량을 매우 크게 늘림. 그래서 미는 힘도 같은 비율로 증가
        _bossAnimation.SkillAnimation();
        _bossRigid.AddForce(_boss.transform.forward * 1000000f, ForceMode.Impulse);

        yield return new WaitForSeconds(1.1f);

        _Sleep = new WaitForSeconds(0.65f);


        if (CurHp > 0)
        {
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
                    instance.transform.SetParent(null);
                    // 일정 랜덤 각도(예시 5~10)씩 틀어지며 탄 생성
                    randomNum = UnityEngine.Random.Range(5, 10);
                    // randomNum = 10;
                    maxAngle += randomNum;
                    instance.SetActive(true);
                    instance.transform.position = new Vector3(_boss.transform.position.x, 0.4f, _boss.transform.position.z);
                    instance.transform.rotation = _boss.transform.rotation;
                    instance.transform.Rotate(Vector3.up, maxAngle);
                    // 생성된 탄환 발사
                    instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * 15f, ForceMode.Impulse);
                }

                yield return _Sleep;
            }
        }
    }

    public void TakeHit(int attackPoint)
    {
        if (CurHp <= 0)
            return;

        CurHp -= attackPoint;

        if (CurHp <= 0)
        {
            CurHp = 0;
            _bossAnimation.DyingAnimation();
            Destroy(gameObject, 3f);
            OnDied.Invoke();
        }
    }
}
