using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    //몬스터수
    [SerializeField] public int monsterCount = 10;
    //몬스터 생성 지연시간
    [SerializeField] public float spawnInterval = 0.3f;
}
