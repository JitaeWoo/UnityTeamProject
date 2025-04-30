using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    //플레이어 위치를 위한 선언
    public Transform player;
    //카메라 위치를 위한 선언
    private Camera mainCamera;

    //몬스터 리스트 생성
    private List<GameObject> currentMonsters = new List<GameObject>();

    private void Start()
    {
        mainCamera = Camera.main;
        
        //웨이브 시작
        StartCoroutine(SpawnWave());

        
    }

    private void Update()
    {
        
    }

    private Vector3 GetSpawnPosition()
    {   
        //메인 카메라 위치
        Vector3 cameraPos = Camera.main.transform.position;
        //맵 크기
        Bounds bounds = mapBounds.bounds;

        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minZ = bounds.min.z;
        float maxZ = bounds.max.z;


        //스폰 좌표 변수 초기화
        float x;
        float z;
        //실패한 시도 회수(무한 루프 방지)
        int loop = 0;

        //맞는 좌표를 찾을 때까지 반복
        while (true)
        {
            //true면 앞뒤 방향 기준으로 거리를 띄우고 false면 좌우 방향으로 거리를 띄움
            bool isVertical = Random.value > 0.5f;
            //방향이 플레이어 기준 위,오른쪽인지 아래,왼쪽인지 정함
            bool isPositive = Random.value > 0.5f;

            //50번 시도했으면 포기
            if (loop >= 50)
            {
                Debug.Log("Fail GetSpawnPosition");
                return Vector3.zero;
            }
            //true면 앞뒤로 떨어뜨림
            if (isVertical)
            {
                //플레이어 위아래로 20만큼 떨어뜨림
                z = 20;
                //좌우는 -30과 30을 랜덤으로 주어짐
                x = Random.Range(-30f, 30f);
                //x 좌표 입력
                x += cameraPos.x;

                //생성이 안되면 
                if (!isPositive)
                {
                    //반대쪽
                    z *= -1;
                }
                //위치 입력
                z += cameraPos.z;
            }
            //false면 좌우로 떨어뜨림
            else
            {
                //플레이어 위 아래로 랜덤
                z = Random.Range(-20f, 20f);
                //위치 입력
                z += cameraPos.z;
                //좌우로 30고정
                x = 30;
                //생성이 안되면
                if (!isPositive)
                {
                    //반대쪽
                    x *= -1;
                }
                //좌표 입력
                x += cameraPos.x;
            }

            //맵 안에 있을때
            if (x < minX || x > maxX || z < minZ || z > maxZ)
            {
                //시도 추가
                loop++;
                continue;
            }
            else
            {
                //좌표값 반환
                return new Vector3(x, 0, z);
            }
        }

    }

    //코루틴으로 웨이브를 만듬
    private IEnumerator SpawnWave()
    {
        //배열 범위를 벗어나면 코루틴 종료
        if (currentWaveIndex >= waves.Length)
        {
            Debug.LogWarning("모든 웨이브 완료됨: currentWaveIndex = " + currentWaveIndex);
            Debug.LogWarning(waves.Length);
            yield break;
        }
        //웨이브 배열에서 현재 웨이브를 가져옴
        Wave currentWave = waves[currentWaveIndex];

        //웨이브 시간
        float waveStartTime = Time.time;
        
        //웨이브는 0부터 몬스터 카운트까지
        for (int i = 0; i < currentWave.monsterCount; i++)
        {

            //스폰 포지션을 가져옴
            Vector3 spwanposition = GetSpawnPosition();
            //몬스터프리팹에서 스폰 포인트에 몬스터를 가져와서 생성함
            GameObject monster = Instantiate(monsterPrefab, spwanposition, Quaternion.identity);
                        
            //그 몬스터를 추가함
            currentMonsters.Add(monster);
            //생성 지연
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }
        while(true)
        {
            //주어진 웨이브 시간이 완료되거나 몬스터가 없다면
            if (Time.time - waveStartTime >=currentWave.timeLimit || currentMonsters.Count == 0)
            { 
                //빠져나옴
                break; 
            }
            //아무것도 안하고 null을 반환함
            yield return null;
        }
        //웨이브 단계값을 하나 올림
        currentWaveIndex++;
        //현재 웨이브가 웨이브배열의 길이보다 작다면
        if (currentWaveIndex < waves.Length)
        {
            //웨이브를 시작함
            Debug.Log($"{currentWaveIndex} 웨이브를 시작합니다.");
            StartCoroutine(SpawnWave());
        }
        else
        {
            //웨이브 배열의 길이와 같다면(모든 웨이브를 클리어했다) 다음씬으로 넘어감
            Debug.Log("모든 웨이브 클리어!");
            LoadNextScene();
        }

    }

    //몬스터가 죽을때 이 함수를 호출해주세요~~
    public void OnMonsterKilled(GameObject monster)
    {
        //리스트에서 몬스터를 제거함
        currentMonsters.Remove(monster);        
    }

    private void LoadNextScene()
    {
        //빌드인덱스에서 씬을 가져옴
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //지금씬에서 하나 더한 것이 다음씬
        int nextSceneIndex = currentSceneIndex + 1;

        //만일 다음씬이 빌드세팅에 셋팅된 씬들의 값보다 작다면
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            //다음 씬으로 넘어감
            Debug.Log("다음 씬으로 넘어갑니다.");
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            //그렇지 않다면 다음씬이 없으므로 게임을 종료함
            Debug.Log("다음 씬이 존재하지 않습니다. 게임종료합니다.");
        }

    }
}
