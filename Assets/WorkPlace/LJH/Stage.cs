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

        Vector3 spawnPos = Vector3.zero;
        //스폰 가능 지점을 false로 초기화함
        bool validPosition = false;

        //스폰 가능 지점이 아닐때까지
        while (!validPosition)
        {
            //랜덤한 숫자를 뽑아서 0.5보다 작으면 -1 크거나 같으면 1을 side에 넣음
            float side = Random.value < 0.5f ? -1f : 1f;
            //x:side 값이 -1이면 -0.1로 화면 왼쪽 밖, 1이면 1.1로 화면의 오른쪽 밖에서 등장하도록 설정
            //y:화면 아래쪽 20%부터 위쪽 80% 사이에 랜덤으로 나타나게함
            //z:카메라가 볼 수 있는 거리 안쪽에 생성되도록 함
            Vector3 viewportPos = new Vector3(side < 0 ? -0.1f : 1.1f, Random.Range(0.2f, 0.8f), mainCamera.nearClipPlane + 5);
            //viewportPos를 월드 좌표로 변환
            spawnPos = mainCamera.ViewportToWorldPoint(viewportPos);
            //높이는 매직넘버가 아닌 변수로 설정
            spawnPos.y = spawnHeight;

            //플레이어와 거리가 충분히 거리가 있는지 확인
            bool distanceOk = Vector3.Distance(spawnPos, player.position) >= spawnDistanceFromPlayer;
            //맵 안에 스폰지점이 있는지 확인
            bool inSideMap = mapBounds.bounds.Contains(new Vector3(spawnPos.x, mapBounds.bounds.center.y, spawnPos.z));

            //두가지 조건이 확인되었다면
            if(distanceOk && inSideMap)
            {
                //스폰 가능한 지점을 true로 바꿔줌
                validPosition = true;
            }
        }
        //그 위치를 반환함
        return spawnPos;

    }

    //코루틴으로 웨이브를 만듬
    private IEnumerator SpawnWave()
    {
        //배열 범위를 벗어나면 코루틴 종료
        if (currentWaveIndex >= waves.Length)
        {
            Debug.LogWarning("모든 웨이브 완료됨: currentWaveIndex = " + currentWaveIndex);
            yield break;
        }
        //웨이브 배열에서 현재 웨이브를 가져옴
        Wave currentWave = waves[currentWaveIndex];
        
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
    }

    //몬스터가 죽을때 이 함수를 호출해주세요~~
    public void OnMonsterKilled(GameObject monster)
    {
        //리스트에서 몬스터를 제거함
        currentMonsters.Remove(monster);
        //몬스터가 없다면
        if (currentMonsters.Count == 0)
        {
            //웨이브 단계값을 하나 올림
            currentWaveIndex++;
            //현재 웨이브가 웨이브의 길이보다 작다면
            if (currentWaveIndex < waves.Length)
            {
                //웨이브를 시작함
                StartCoroutine(SpawnWave());
            }
            else
            {
                //모든 웨이브를 클리어 했다면 다음씬으로 넘어감
                Debug.Log("모든 웨이브 클리어!");
                LoadNextScene();
            }
        }
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
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            //그렇지 않다면 다음씬이 없으므로 게임을 종료함
            Debug.Log("다음 씬이 존재하지 않습니다. 게임종료합니다.");
        }

    }
}
