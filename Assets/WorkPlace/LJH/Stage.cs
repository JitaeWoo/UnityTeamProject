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


    //������ ������ ���� ����
    public Collider mapBounds;
    //�÷��̾� ��ġ�� ���� ����
    public Transform player;
    
    private void Start()
    {
        //�ؽ�Ʈ �����������̸��� �ν����Ϳ��� �޾Ƽ� �޴��������� ����
        Manager.Stage.NextScene = nextStageSceneName;
        //���̺� ����
        StartCoroutine(SpawnWave());

    }

    private Vector3 GetSpawnPosition()
    {   
        //���� ī�޶� ��ġ
        Vector3 cameraPos = Camera.main.transform.position;
        //�� ũ��
        Bounds bounds = mapBounds.bounds;

        float minX = bounds.min.x+5;
        float maxX = bounds.max.x-5;
        float minZ = bounds.min.z+5;
        float maxZ = bounds.max.z-5;


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
                return new Vector3(x, 1, z);
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
        
    }

    private void CreateMonster(Vector3 position, GameObject prefab)
    {
        //���� ���� �ϳ� ����
        _monsterCount++;
        //���� ������
        GameObject monster = Instantiate(prefab, position, Quaternion.identity);
        //���Ͱ� �ı��Ǿ��� ��

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

                //���� ���� �ϳ� ����
                _monsterCount--;
                //��� ���Ͱ� �ı��Ǿ��ٸ�
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
                //���� ���� �ϳ� ����
                _monsterCount--;
                //��� ���Ͱ� �ı��Ǿ��ٸ�
                if (_monsterCount == 0 && _isAllMonsterSpawned)
                {
                    //���� ������ �Ѿ
                    Manager.Game.SceneChange("NextScene");
                }

                Destroy(bossHp.gameObject);
            };
        }
    }

}
