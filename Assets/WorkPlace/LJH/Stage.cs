using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    ////OnStageStart 스테이지 시작

    ////웨이브 시작

    ////몬스터 개체수가 0일때 클리어
    //while(monsterNum != 0)
    //{
    //    //IsWaveClear = true;
    //    //웨이브
    //    if (!finalWave)
    //    {
    //        naxtWave();
    //    }
    //    else
    //    {
    //        //OnStageClear 스테이지 클리어

    //        //클리어 UI화면 호출
    //        StageClaerUI();
    //        //캐릭터 강화 선택

    //        //만일 마지막 스테이지면
    //        if (finalStage)
    //        {
    //            gameOver();
    //        }
    //        //마지막 스테이지가 아니라면
    //        else
    //        {
    //            //클리어시 다음 스테이지로 이동
    //            nextStage();
    //        }
    //    }
    //}
    //몬스터수 초기화
    //[SerializeField] private int monsterCount = 10;
    //지연시간 초기화    
    //[SerializeField] private float spawnInterval = 0.3f;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Wave[] waves;
    [SerializeField] public int currentWaveIndex;

    private List<GameObject> currentMonsters = new List<GameObject>();

    private void Start()
    {
        //웨이브 시작
        StartCoroutine(SpawnWave());
    }

    //코루틴으로 웨이브를 만듬
    private IEnumerator SpawnWave()
    {
        //웨이브 배열에서 현재 웨이브를 가져옴
        Wave currentWave = waves[currentWaveIndex];

        //웨이브는 0부터 몬스터 카운트까지
        for (int i = 0; i < currentWave.monsterCount; i++)
        {
            //스폰 포인트길이 만큼의 수 중에서 랜덤의 위치를 만듦
            Transform spwanPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            //몬스터프리팹에서 스폰 포인트에 몬스터를 가져와서 생성함
            GameObject monster = Instantiate(monsterPrefab, spwanPoint.position, Quaternion.identity);
            //그 몬스터를 추가함
            currentMonsters.Add(monster);
            //생성 지연
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }
    }

    public void OnMonsterKilled(GameObject monster)
    {
        currentMonsters.Remove(monster);
        if (currentMonsters.Count == 0)
        {
            currentWaveIndex++;
            if (currentWaveIndex < waves.Length)
            {
                StartCoroutine(SpawnWave());
            }
            else
            {
                Debug.Log("모든 웨이브 클리어!");
            }
        }
    }

}
