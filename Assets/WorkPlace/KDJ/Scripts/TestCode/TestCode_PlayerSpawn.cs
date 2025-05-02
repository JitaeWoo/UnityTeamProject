using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode_PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        Manager.Player.CreatePlayer(this.transform.position);
    }
}
