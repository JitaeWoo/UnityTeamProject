using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject wallPrefab;
    public int width = 100;
    public int height = 100;
    public int mapStartXPos = -101;
    public int mapStartZPos = -101;
    public int FloorPos = -1;
    [SerializeField] public GameObject[] groundPrefabs;

    void Awake()
    {
        for (int x = mapStartXPos; x <= width+1; x++)
        {
            for (int z = mapStartZPos; z <= height+1; z++)
            {

                GameObject selecteGround = groundPrefabs[Random.Range(0, groundPrefabs.Length)];

                Vector3 groundpos = new Vector3(x, FloorPos, z);
                Instantiate(groundPrefab, groundpos, Quaternion.identity, this.transform);
                for (int y = 0; y < 4; y++)
                {
                    if (x== mapStartXPos || x== width+1||z== mapStartZPos || z== height+1)
                    { 
                        Vector3 wallpos = new Vector3(x, y, z);
                        Instantiate(wallPrefab, wallpos, Quaternion.identity, this.transform);
                    }
                }
            }
        }
    }
}
