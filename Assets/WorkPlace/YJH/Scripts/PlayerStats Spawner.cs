using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsSpawner : MonoBehaviour
{
    public GameObject playerStatPopup;
    public GameObject Blocker;
    private string canvas = "UI_Canvas";
   public void SpawnPlayerStats()
    {
        GameObject spawnLocation = GameObject.Find(canvas);
        Instantiate(playerStatPopup, spawnLocation.transform);
    }
}
