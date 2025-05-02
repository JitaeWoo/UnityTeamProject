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
        Manager.Player.CreatePlayer(_playerSpawnPoint.position);

        SpawnSlot = new Stack<MonsterInitiation>(_monsterSpawnpoint.Length);
        for (int cnt = 0; cnt < _monsterSpawnpoint.Length; cnt++)
        {
            GameObject monster = Instantiate(_monsterPrefabList[cnt]);
            monster.SetActive(false);
            MonsterInitiation monsterInitiation = new MonsterInitiation { Monster = monster, SpawnPoint = _monsterSpawnpoint[cnt] };
            SpawnSlot.Push(monsterInitiation);
        }
    }

    void OnEnable()
    {
        // SpawnSlot.Count°¡ 0ÀÓ. ¿Ö???
        for (int cnt = 0; cnt < SpawnSlot.Count; cnt++)
        {
            if (SpawnSlot.TryPop(out MonsterInitiation monster))
            {
                monster.Monster.SetActive(true);
                monster.Monster.transform.position = monster.SpawnPoint.position;
            }
        }
    }
}
