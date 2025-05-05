using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    [SerializeField] public int currentWaveIndex;
    [SerializeField] private int _monsterCount = 0;
    [SerializeField] private bool _isAllMonsterSpawned;
    [SerializeField] private string nextStageSceneName;
    [SerializeField] private GameObject Upgrader;
    [SerializeField] private Slider _bossHp;


    //생성한 맵위에 몬스터 생성
    public Collider mapBounds;
    //플레이어 위치를 위한 선언
    public Transform player;
    
    private void Start()
    {
        //넥스트 스테이지씬이름을 인스펙터에서 받아서 메니저변수에 넣음
        Manager.Stage.NextScene = nextStageSceneName;
        //웨이브 시작
        StartCoroutine(SpawnWave());

    }

    private Vector3 GetSpawnPosition()
    {   
        //메인 카메라 위치
        Vector3 cameraPos = Camera.main.transform.position;
        //맵 크기
        Bounds bounds = mapBounds.bounds;

        float minX = bounds.min.x+5;
        float maxX = bounds.max.x-5;
        float minZ = bounds.min.z+5;
        float maxZ = bounds.max.z-5;


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
                return new Vector3(x, 1, z);
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

        //웨이브 몬스터 그룹 전체
        foreach (var group in currentWave.WaveMonsters)
        {
            //그룹의 수까지
            for (int i = 0; i < group.Count; i++)
            {
                //스폰 포지션을 가져옴
                Vector3 spwanposition = GetSpawnPosition();
                //생성되고 파괴된 몬스터 카운팅
                CreateMonster(spwanposition, group.MonsterPrefab);
                //생성 지연
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
        }

        if (currentWaveIndex == waves.Length - 1)
        {
            _isAllMonsterSpawned = true;
        }

        while(true)
        {
            //주어진 웨이브 시간이 완료되거나 몬스터가 없다면
            if (Time.time - waveStartTime >=currentWave.timeLimit || _monsterCount == 0)
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
        
    }

    private void CreateMonster(Vector3 position, GameObject prefab)
    {
        //몬스터 수를 하나 증가
        _monsterCount++;
        //몬스터 생성함
        GameObject monster = Instantiate(prefab, position, Quaternion.identity);
        //몬스터가 파괴되었을 때

        MonsterController monsterController = monster.GetComponent<MonsterController>();

        if (monsterController != null)
        {
            Slider bossHp = null;
            if (monsterController.IsMiniBoss)
            {
                bossHp = Instantiate(_bossHp, FindObjectOfType<Canvas>().transform);
                bossHp.maxValue = monsterController.MaxHp;
                bossHp.value = monsterController.CurHp;
                monsterController.OnChangeCurHp.AddListener(() =>
                {
                    if(bossHp != null)
                    {
                        bossHp.value = monsterController.CurHp;
                    }
                });
            }

            monsterController.OnDied.AddListener(() =>
            {
                if (bossHp != null)
                {
                    Destroy(bossHp.gameObject);
                }

                //몬스터 수를 하나 감소
                _monsterCount--;
                //모든 몬스터가 파괴되었다면
                if (_monsterCount == 0 && _isAllMonsterSpawned)
                {
                    Instantiate(Upgrader, FindObjectOfType<Canvas>().transform);
                }
            });
        }

        BossController bossController = monster.GetComponent<BossController>();

        if (bossController != null)
        {
            Slider bossHp = Instantiate(_bossHp, FindObjectOfType<Canvas>().transform);
            bossHp.maxValue = bossController.MaxHp;
            bossHp.value = bossController.CurHp;
            Debug.Log(bossController.CurHp);
            bossController.OnCurHpChanged += () =>
            {
                Debug.Log("ddd");
                if (bossHp != null)
                {
                    bossHp.value = bossController.CurHp;
                }
            };

            bossController.OnDied += () =>
            {
                //몬스터 수를 하나 감소
                _monsterCount--;
                //모든 몬스터가 파괴되었다면
                if (_monsterCount == 0 && _isAllMonsterSpawned)
                {
                    //다음 씬으로 넘어감
                    Manager.Game.SceneChange("NextScene");
                }

                Destroy(bossHp.gameObject);
            };
        }
    }

}
