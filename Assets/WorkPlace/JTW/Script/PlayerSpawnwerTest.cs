using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnwerTest : MonoBehaviour
{
    void Start()
    {
        Manager.Player.CreatePlayer(Vector3.zero);
    }
}
