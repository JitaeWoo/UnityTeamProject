using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave
{
    //���ͼ�
    [SerializeField] public int monsterCount;
    //���� ���� �����ð�
    [SerializeField] public float spawnInterval;
    //���̺� �ð�
    [SerializeField] public float timeLimit;
}
