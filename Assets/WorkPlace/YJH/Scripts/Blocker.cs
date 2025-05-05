using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    GameObject spawnedBlocker;

    public void Awake()
    {
        spawnedBlocker = gameObject;
        RectTransform rt = spawnedBlocker.GetComponent<RectTransform>(); // 블로커의 RECTTRANSFORM 가져옴

        // 블로커가 전체 화면을 덮도록 초기화
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = Vector2.zero;
    }
}
