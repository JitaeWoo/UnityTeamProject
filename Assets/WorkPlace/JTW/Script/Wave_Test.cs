using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave_Test
{
    //몬스터수
    [SerializeField] public int monsterCount = 10;
    //몬스터 생성 지연시간
    [SerializeField] public float spawnInterval = 0.3f;

    // 웨이브에서 생성될 몬스터 그룹
    public MonsterGroup[] WaveMonsters;
}
