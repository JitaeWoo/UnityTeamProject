using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode_DamageZone : MonoBehaviour
{
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 20f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.GetComponent<PlayerController>().TakeHit(1);
        }
    }
}
