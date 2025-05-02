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
        // ���� ���� ����(���� 5~10)�� Ʋ������ ź����
        // ���� ������ 360�� ������ ��������
        // ������ źȯ �߻�
        // �ݺ�
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
