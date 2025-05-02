using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    enum AttackPattern
    {
        BulletHell, DashAttack
    }

    [SerializeField] private GameObject _bulletPrefab;

    private Stack<GameObject> _bulletPool;
    private GameObject _player;


    private void Awake()
    {
        Init();
    }

    void BulletHell()
    {
        // 일정 랜덤 각도(예시 5~10)씩 틀어지며 탄생성
        // 누적 각도가 360이 넘으면 생성중지
        // 생성된 탄환 발사
        // 반복
    }

    void DashAttack()
    {

    }

    void Init()
    {
        _player = Manager.Player.Player;
        

        _bulletPool = new Stack<GameObject>(200);

        for (int i = 0; i < 200; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.GetComponent<Bullet>().returnPool = _bulletPool;
            bullet.SetActive(false);
            _bulletPool.Push(bullet);
        }
    }
}
