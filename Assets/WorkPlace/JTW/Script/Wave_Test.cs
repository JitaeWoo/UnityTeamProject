using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave_Test
{
    //���ͼ�
    [SerializeField] public int monsterCount = 10;
    //���� ���� �����ð�
    [SerializeField] public float spawnInterval = 0.3f;

    // ���̺꿡�� ������ ���� �׷�
    public MonsterGroup[] WaveMonsters;
}
