using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    ////OnStageStart �������� ����

    ////���̺� ����

    ////���� ��ü���� 0�϶� Ŭ����
    //while(monsterNum != 0)
    //{
    //    //IsWaveClear = true;
    //    //���̺�
    //    if (!finalWave)
    //    {
    //        naxtWave();
    //    }
    //    else
    //    {
    //        //OnStageClear �������� Ŭ����

    //        //Ŭ���� UIȭ�� ȣ��
    //        StageClaerUI();
    //        //ĳ���� ��ȭ ����

    //        //���� ������ ����������
    //        if (finalStage)
    //        {
    //            gameOver();
    //        }
    //        //������ ���������� �ƴ϶��
    //        else
    //        {
    //            //Ŭ����� ���� ���������� �̵�
    //            nextStage();
    //        }
    //    }
    //}
    //���ͼ� �ʱ�ȭ
    //[SerializeField] private int monsterCount = 10;
    //�����ð� �ʱ�ȭ    
    //[SerializeField] private float spawnInterval = 0.3f;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Wave[] waves;
    [SerializeField] public int currentWaveIndex;

    private List<GameObject> currentMonsters = new List<GameObject>();

    private void Start()
    {
        //���̺� ����
        StartCoroutine(SpawnWave());
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
