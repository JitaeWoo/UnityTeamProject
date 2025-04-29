using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject _grenade;
    [SerializeField] private GameObject _explosion;

    private void OnCollisionEnter(Collision collision)
    {
        // Ãæµ¹½Ã ÆÄ±«ÈÄ Æø¹ß ÀÌÆåÆ® »ý¼º
        _explosion.transform.position = _grenade.transform.position;
        Destroy(_grenade);
        Instantiate(_explosion);
    }
}
