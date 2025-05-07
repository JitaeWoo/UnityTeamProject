using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject _grenade;
    [SerializeField] private GameObject _explosion;
    [SerializeField] public int grenadeDamage;

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� �ı��� ���� ����Ʈ ����
        if (other.gameObject.layer != 7 && other.gameObject.layer != 8 && (other.GetComponentInParent<MonsterController>()?.CurHp > 0 || other.GetComponentInParent<BossController>()?.CurHp > 0))
        {
            _explosion.transform.position = _grenade.transform.position;
            Destroy(_grenade);
            Instantiate(_explosion);
        }
    }
}
