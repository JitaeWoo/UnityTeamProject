using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    GameObject spawnedBlocker;

    public void Awake()
    {
        spawnedBlocker = gameObject;
        RectTransform rt = spawnedBlocker.GetComponent<RectTransform>(); // ���Ŀ�� RECTTRANSFORM ������

        // ���Ŀ�� ��ü ȭ���� ������ �ʱ�ȭ
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.anchoredPosition = Vector2.zero;
        rt.sizeDelta = Vector2.zero;
    }
}
