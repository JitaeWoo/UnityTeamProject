using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletRigid;

    public Stack<GameObject> returnPool;

    void OnEnable()
    {
        StartCoroutine(ReturnBullet(3f));
    }

    IEnumerator ReturnBullet(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        _bulletRigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
        returnPool.Push(gameObject);
    }
}
