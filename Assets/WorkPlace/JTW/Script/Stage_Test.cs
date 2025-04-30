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

        //������ ������ ���� ����
        public Collider mapBounds;
        //�÷��̾� ��ġ�� ���� ����
        public Transform player;
        //ī�޶� ��ġ�� ���� ����
        private Camera mainCamera;

        //���� ����Ʈ ����
        private List<GameObject> currentMonsters = new List<GameObject>();

        private void Start()
        {
            mainCamera = Camera.main;

            //���̺� ����
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

        //�ڷ�ƾ���� ���̺긦 ����
        private IEnumerator SpawnWave()
        {
            //�迭 ������ ����� �ڷ�ƾ ����
            if (currentWaveIndex >= waves.Length)
            {
                Debug.LogWarning("��� ���̺� �Ϸ��: currentWaveIndex = " + currentWaveIndex);
                Debug.LogWarning(waves.Length);
                yield break;
            }
            //���̺� �迭���� ���� ���̺긦 ������
            Wave_Test currentWave = waves[currentWaveIndex];

            //���̺�� 0���� ���� ī��Ʈ����
            for (int i = 0; i < currentWave.monsterCount; i++)
            {

                //���� �������� ������
                Vector3 spwanposition = GetSpawnPosition();
                //���������տ��� ���� ����Ʈ�� ���͸� �����ͼ� ������
                GameObject monster = Instantiate(monsterPrefab, spwanposition, Quaternion.identity);

                //�� ���͸� �߰���
                currentMonsters.Add(monster);
                //���� ����
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
        }

        //���Ͱ� ������ �� �Լ��� ȣ�����ּ���~~
        public void OnMonsterKilled(GameObject monster)
        {
            //����Ʈ���� ���͸� ������
            currentMonsters.Remove(monster);
            //���Ͱ� ���ٸ�
            if (currentMonsters.Count == 0)
            {
                //���̺� �ܰ谪�� �ϳ� �ø�
                currentWaveIndex++;
                //���� ���̺갡 ���̺�迭�� ���̺��� �۴ٸ�
                if (currentWaveIndex < waves.Length)
                {
                    //���̺긦 ������
                    Debug.Log($"{currentWaveIndex} ���̺긦 �����մϴ�.");
                    StartCoroutine(SpawnWave());
                }
                else
                {
                    //���̺� �迭�� ���̿� ���ٸ�(��� ���̺긦 Ŭ�����ߴ�) ���������� �Ѿ
                    Debug.Log("��� ���̺� Ŭ����!");
                    LoadNextScene();
                }
            }
        }

        private void LoadNextScene()
        {
            //�����ε������� ���� ������
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            //���ݾ����� �ϳ� ���� ���� ������
            int nextSceneIndex = currentSceneIndex + 1;

            //���� �������� ���弼�ÿ� ���õ� ������ ������ �۴ٸ�
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                //���� ������ �Ѿ
                Debug.Log("���� ������ �Ѿ�ϴ�.");
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                //�׷��� �ʴٸ� �������� �����Ƿ� ������ ������
                Debug.Log("���� ���� �������� �ʽ��ϴ�. ���������մϴ�.");
            }

        }
    }
}

