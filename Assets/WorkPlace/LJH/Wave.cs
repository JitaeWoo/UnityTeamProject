using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Wave
{
    //몬스터 생성 지연시간
    [SerializeField] public float spawnInterval;
    //웨이브 시간
    [SerializeField] public float timeLimit;
    //몬스터 그룹
    [SerializeField] public MonsterGroup[] WaveMonsters;
}
