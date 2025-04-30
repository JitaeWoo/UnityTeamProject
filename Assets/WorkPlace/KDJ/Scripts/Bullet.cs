using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        // ���� �浹�� ����
        if (other.gameObject.layer == 6)
        {
            StartCoroutine(ReturnBullet());
        }

        // ���Ϳ� �浹 �� ���� ���
        if(other.gameObject.tag == "Monster")
        {
            if(PierceNum > 0)
            {
                PierceNum--;
            }
            else
            {
                StartCoroutine(ReturnBullet());
            }
        }
    }

    void HomingBullet()
    {
        // ���� �̱���
    }

    IEnumerator ReturnBullet(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        _bulletRigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
        returnPool.Push(gameObject);
    }
}
