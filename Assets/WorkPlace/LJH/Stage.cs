using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Stage : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Wave[] waves;
    [SerializeField] public int currentWaveIndex;
    [SerializeField] public float spawnDistanceFromPlayer = 5f;
    [SerializeField] private float spawnHeight = 0f;
    [SerializeField] private int _monsterCount = 0;
    [SerializeField] private bool _isAllMonsterSpawned;

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
        //���� ī�޶� ��ġ
        Vector3 cameraPos = Camera.main.transform.position;
        //�� ũ��
        Bounds bounds = mapBounds.bounds;

        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minZ = bounds.min.z;
        float maxZ = bounds.max.z;


        //���� ��ǥ ���� �ʱ�ȭ
        float x;
        float z;
        //������ �õ� ȸ��(���� ���� ����)
        int loop = 0;

        //�´� ��ǥ�� ã�� ������ �ݺ�
        while (true)
        {
            //true�� �յ� ���� �������� �Ÿ��� ���� false�� �¿� �������� �Ÿ��� ���
            bool isVertical = Random.value > 0.5f;
            //������ �÷��̾� ���� ��,���������� �Ʒ�,�������� ����
            bool isPositive = Random.value > 0.5f;

            //50�� �õ������� ����
            if (loop >= 50)
            {
                Debug.Log("Fail GetSpawnPosition");
                return Vector3.zero;
            }
            //true�� �յڷ� ����߸�
            if (isVertical)
            {
                //�÷��̾� ���Ʒ��� 20��ŭ ����߸�
                z = 20;
                //�¿�� -30�� 30�� �������� �־���
                x = Random.Range(-30f, 30f);
                //x ��ǥ �Է�
                x += cameraPos.x;

                //������ �ȵǸ� 
                if (!isPositive)
                {
                    //�ݴ���
                    z *= -1;
                }
                //��ġ �Է�
                z += cameraPos.z;
            }
            //false�� �¿�� ����߸�
            else
            {
                //�÷��̾� �� �Ʒ��� ����
                z = Random.Range(-20f, 20f);
                //��ġ �Է�
                z += cameraPos.z;
                //�¿�� 30����
                x = 30;
                //������ �ȵǸ�
                if (!isPositive)
                {
                    //�ݴ���
                    x *= -1;
                }
                //��ǥ �Է�
                x += cameraPos.x;
            }

            //�� �ȿ� ������
            if (x < minX || x > maxX || z < minZ || z > maxZ)
            {
                //�õ� �߰�
                loop++;
                continue;
            }
            else
            {
                //��ǥ�� ��ȯ
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
        Wave currentWave = waves[currentWaveIndex];

        //���̺� �ð�
        float waveStartTime = Time.time;

        //���̺� ���� �׷� ��ü
        foreach (var group in currentWave.WaveMonsters)
        {
            //�׷��� ������
            for (int i = 0; i < group.Count; i++)
            {
                //���� �������� ������
                Vector3 spwanposition = GetSpawnPosition();
                //�����ǰ� �ı��� ���� ī����
                CreateMonster(spwanposition, group.MonsterPrefab);
                //���� ����
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
        }

        if (currentWaveIndex == waves.Length - 1)
        {
            _isAllMonsterSpawned = true;
        }

        while(true)
        {
            //�־��� ���̺� �ð��� �Ϸ�ǰų� ���Ͱ� ���ٸ�
            if (Time.time - waveStartTime >=currentWave.timeLimit || _monsterCount == 0)
            { 
                //��������
                break; 
            }
            //�ƹ��͵� ���ϰ� null�� ��ȯ��
            yield return null;
        }
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

    private void CreateMonster(Vector3 position, GameObject prefab)
    {
        //���� ���� �ϳ� ����
        _monsterCount++;
        //���� ������
        GameObject monster = Instantiate(prefab, position, Quaternion.identity);
        //���Ͱ� �ı��Ǿ��� ��
        monster.GetComponent<MonsterController>().OnDied += () =>
        {
            //���� ���� �ϳ� ����
            _monsterCount--;
            //��� ���Ͱ� �ı��Ǿ��ٸ�
            if (_monsterCount == 0 && _isAllMonsterSpawned)
            {
                //���� ������ �Ѿ
                Manager.Game.SceneChange("NextScene");
            }
        };

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
