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

    //������ ������ ���� ����
    public Collider mapBounds;

    public Transform player;
    private Camera mainCamera;


    private List<GameObject> currentMonsters = new List<GameObject>();

    private void Start()
    {
        mainCamera = Camera.main;
        //���̺� ����
        StartCoroutine(SpawnWave());
    }

    private void SpawnMonster()
    {
        Vector3 spwanposition = GetSpawnPosition();
        //���������տ��� ���� ����Ʈ�� ���͸� �����ͼ� ������
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

    //�ڷ�ƾ���� ���̺긦 ����
    private IEnumerator SpawnWave()
    {
        //���̺� �迭���� ���� ���̺긦 ������
        Wave currentWave = waves[currentWaveIndex];

        //���̺�� 0���� ���� ī��Ʈ����
        for (int i = 0; i < currentWave.monsterCount; i++)
        {
            //���� ����Ʈ���� ��ŭ�� �� �߿��� ������ ��ġ�� ����
            Transform spwanPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            
            //���������տ��� ���� ����Ʈ�� ���͸� �����ͼ� ������
            GameObject monster = Instantiate(monsterPrefab, spwanPoint.position, Quaternion.identity);
            //�� ���͸� �߰���
            currentMonsters.Add(monster);
            //���� ����
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
                Debug.Log("��� ���̺� Ŭ����!");
            }
        }
    }

}
