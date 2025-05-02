using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public struct MonsterInitiation {
    public GameObject Monster;
    public Transform SpawnPoint;
}

public class TestStageManagerCYE : MonoBehaviour
{
    [SerializeField] private GameObject[] _monsterPrefabList;
    [SerializeField] private Transform[] _monsterSpawnpoint;
    [SerializeField] private Transform _playerSpawnPoint;
    private Stack<MonsterInitiation> SpawnSlot = new Stack<MonsterInitiation>();

    private void Start()
    {
        SpawnSlot = new Stack<MonsterInitiation>(_monsterSpawnpoint.Length);
        for (int cnt = 0; cnt < _monsterSpawnpoint.Length; cnt++)
        {
            MonsterInitiation monsterInitiation = new MonsterInitiation { Monster = Instantiate(_monsterPrefabList[cnt]), SpawnPoint = _monsterSpawnpoint[cnt] };
            monsterInitiation.Monster.SetActive(false);
            SpawnSlot.Push(monsterInitiation);
        }
    }

    void OnEnable()
    {
        Manager.Player.CreatePlayer(_playerSpawnPoint.position);

        foreach (MonsterInitiation monster in SpawnSlot) {
            Debug.Log("log");
            monster.Monster.SetActive(true);
            monster.Monster.transform.position = monster.SpawnPoint.position;
        }
    }
}
