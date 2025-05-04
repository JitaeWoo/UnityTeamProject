using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner_Title : MonoBehaviour
{
    public GameObject manualPrefab; // ������ ������
    public GameObject descriptionPrefab;
    public Transform parentTransform; // ������ �θ�

    public void SpawnManualPopup()
    {
        Instantiate(manualPrefab, parentTransform);
    }

    public void SpawnDescriptionPopup()
    {
        Instantiate(descriptionPrefab, parentTransform);
    }
}
