using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        GrenadeExplosion();
    }


    private void GrenadeExplosion()
    {
        Collider[] monsters = Physics.OverlapSphere(transform.position, 3f);
        for(int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i].gameObject.layer == 9)
            {
                monsters[i].GetComponent<MonsterController>()?.TakeHit(10);
            }
        }
    }
}
