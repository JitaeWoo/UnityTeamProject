using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner_Title : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������
    public Transform parentTransform; // ������ �θ�

    public void SpawnPrefab()
    {
        Instantiate(prefabToSpawn, parentTransform);
    }
}
