using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject _grenade;
    [SerializeField] private GameObject _explosion;

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� �ı��� ���� ����Ʈ ����
        _explosion.transform.position = _grenade.transform.position;
        Destroy(_grenade);
        Instantiate(_explosion);
    }
}
