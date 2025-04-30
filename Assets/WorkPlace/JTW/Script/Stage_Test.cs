using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JTW_Test
{
    public class Stage_Test : MonoBehaviour
    {
        [SerializeField] private GameObject monsterPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Wave_Test[] waves;
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

            Vector3 cameraPos = Camera.main.transform.position;

            float x;
            float z;

            int loop = 0;

            while (true)
            {
                bool isVertical = Random.value > 0.5f;
                bool isPositive = Random.value > 0.5f;

                if (loop >= 50)
                {
                    Debug.Log("Fail GetSpawnPosition");
                    return Vector3.zero;
                }

                if (isVertical)
                {
                    z = 20;
                    x = Random.Range(-30f, 30f);
                    x += cameraPos.x;

                    if (!isPositive)
                    {
                        z *= -1;
                    }

                    z += cameraPos.z;
                }
                else
                {
                    z = Random.Range(-20f, 20f);
                    z += cameraPos.z;
                    x = 30;

                    if (!isPositive)
                    {
                        x *= -1;
                    }

                    x += cameraPos.x;
                }

                if (x < -95 || x > 95 || z < -95 || z > 95)
                {
                    loop++;
                    continue;
                }
                else
                {
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
            Wave_Test currentWave = waves[currentWaveIndex];

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
}

