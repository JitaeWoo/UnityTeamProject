using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // 积己且 橇府普
    public Transform parentTransform; // 积己瞪 何葛

    public void SpawnPrefab()
    {
        Instantiate(prefabToSpawn, parentTransform);
    }
}
