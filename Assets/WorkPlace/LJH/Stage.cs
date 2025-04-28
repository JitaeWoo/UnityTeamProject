using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Wave[] waves;
    [SerializeField] public int currentWaveIndex;
    [SerializeField] public float spawnDistanceFromPlayer = 5f;
    [SerializeField] private float spawnHeight = 0f;

    //생성한 맵위에 몬스터 생성
    public Collider mapBounds;

    public Transform player;
    private Camera mainCamera;


    private List<GameObject> currentMonsters = new List<GameObject>();

    private void Start()
    {
        mainCamera = Camera.main;
        //웨이브 시작
        StartCoroutine(SpawnWave());
    }

    private void SpawnMonster()
    {
        Vector3 spwanposition = GetSpawnPosition();
        //몬스터프리팹에서 스폰 포인트에 몬스터를 가져와서 생성함
        GameObject monster = Instantiate(monsterPrefab, spwanposition, Quaternion.identity);

        Vector3 moveDirection = (player.position - spwanposition).normalized;
        
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPos = Vector3.zero;
        bool validPosition = false;

        while (!validPosition)
        {
            float side = Random.value < 0.5f ? -1f : 1f;

            Vector3 viewportPos = new Vector3(side < 0 ? -0.1f : 1.1f, Random.Range(0.2f, 0.8f), mainCamera.nearClipPlane + 5);
            spawnPos = mainCamera.ViewportToWorldPoint(viewportPos);
            spawnPos.y = spawnHeight;

            bool distanceOk = Vector3.Distance(spawnPos, player.position) >= spawnDistanceFromPlayer;

            bool inSideMap = mapBounds.bounds.Contains(new Vector3(spawnPos.x, mapBounds.bounds.center.y, spawnPos.z));

            if(distanceOk && inSideMap)
            {
                validPosition = true;
            }
        }
        return spawnPos;

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
