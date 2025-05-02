using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnwerTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Manager.Game.SceneChange("Stage1");
        }
    }
}
