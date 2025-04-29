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

        Vector3 spawnPos = Vector3.zero;
        //���� ���� ������ false�� �ʱ�ȭ��
        bool validPosition = false;

        //���� ���� ������ �ƴҶ�����
        while (!validPosition)
        {
            //������ ���ڸ� �̾Ƽ� 0.5���� ������ -1 ũ�ų� ������ 1�� side�� ����
            float side = Random.value < 0.5f ? -1f : 1f;
            //x:side ���� -1�̸� -0.1�� ȭ�� ���� ��, 1�̸� 1.1�� ȭ���� ������ �ۿ��� �����ϵ��� ����
            //y:ȭ�� �Ʒ��� 20%���� ���� 80% ���̿� �������� ��Ÿ������
            //z:ī�޶� �� �� �ִ� �Ÿ� ���ʿ� �����ǵ��� ��
            Vector3 viewportPos = new Vector3(side < 0 ? -0.1f : 1.1f, Random.Range(0.2f, 0.8f), mainCamera.nearClipPlane + 5);
            //viewportPos�� ���� ��ǥ�� ��ȯ
            spawnPos = mainCamera.ViewportToWorldPoint(viewportPos);
            //���̴� �����ѹ��� �ƴ� ������ ����
            spawnPos.y = spawnHeight;

            //�÷��̾�� �Ÿ��� ����� �Ÿ��� �ִ��� Ȯ��
            bool distanceOk = Vector3.Distance(spawnPos, player.position) >= spawnDistanceFromPlayer;
            //�� �ȿ� ���������� �ִ��� Ȯ��
            bool inSideMap = mapBounds.bounds.Contains(new Vector3(spawnPos.x, mapBounds.bounds.center.y, spawnPos.z));

            //�ΰ��� ������ Ȯ�εǾ��ٸ�
            if(distanceOk && inSideMap)
            {
                //���� ������ ������ true�� �ٲ���
                validPosition = true;
            }
        }
        //�� ��ġ�� ��ȯ��
        return spawnPos;

    }

    //�ڷ�ƾ���� ���̺긦 ����
    private IEnumerator SpawnWave()
    {
        //�迭 ������ ����� �ڷ�ƾ ����
        if (currentWaveIndex >= waves.Length)
        {
            Debug.LogWarning("��� ���̺� �Ϸ��: currentWaveIndex = " + currentWaveIndex);
            yield break;
        }
        //���̺� �迭���� ���� ���̺긦 ������
        Wave currentWave = waves[currentWaveIndex];
        
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
            //���� ���̺갡 ���̺��� ���̺��� �۴ٸ�
            if (currentWaveIndex < waves.Length)
            {
                //���̺긦 ������
                StartCoroutine(SpawnWave());
            }
            else
            {
                //��� ���̺긦 Ŭ���� �ߴٸ� ���������� �Ѿ
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
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            //�׷��� �ʴٸ� �������� �����Ƿ� ������ ������
            Debug.Log("���� ���� �������� �ʽ��ϴ�. ���������մϴ�.");
        }

    }
}
