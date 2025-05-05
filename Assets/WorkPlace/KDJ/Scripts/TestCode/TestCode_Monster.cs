using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode_Monster : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log($"{gameObject.name}이 맞았다!");
        }
    }
}
