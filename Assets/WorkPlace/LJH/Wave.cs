using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave
{
    //���� ���� �����ð�
    [SerializeField] public float spawnInterval;
    //���̺� �ð�
    [SerializeField] public float timeLimit;
    //���� �׷�
    [SerializeField] public MonsterGroup[] WaveMonsters;
}
