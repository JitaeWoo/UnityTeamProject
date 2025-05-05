using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpawner_Title : MonoBehaviour
{
    public GameObject manualPrefab; // 积己且 橇府普
    public GameObject descriptionPrefab;
    public Transform parentTransform; // 积己瞪 何葛

    public void SpawnManualPopup()
    {
        Instantiate(manualPrefab, parentTransform);
    }

    public void SpawnDescriptionPopup()
    {
        Instantiate(descriptionPrefab, parentTransform);
    }
}
