using System.Collections;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private GameObject _beam;

    private Coroutine _beamCoroutine;
    private bool CanAttack = false;

    private void Start()
    {
        if (_beamCoroutine == null)
            _beamCoroutine = StartCoroutine(RemoveBeam());
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (CanAttack)
                other.GetComponentInParent<IDamagable>()?.TakeHit(5);
        }
    }

    void BeamModify(float x, float z, float radius)
    {
        _beam.transform.localScale = new Vector3(x, 40, z);
        _beam.GetComponent<CapsuleCollider>().radius = radius;
    }

    IEnumerator RemoveBeam()
    {
        BeamModify(0.05f, 0.05f, 0.025f);
        yield return new WaitForSeconds(0.3f);
        BeamModify(0.3f, 0.3f, 0.15f);
        yield return new WaitForSeconds(0.01f);
        BeamModify(0.5f, 0.5f, 0.25f);
        yield return new WaitForSeconds(0.01f);
        BeamModify(0.75f, 0.75f, 0.375f);
        yield return new WaitForSeconds(0.01f);
        BeamModify(1f, 1f, 0.5f);
        CanAttack = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(_beam.gameObject);
        CanAttack = false;
        _beamCoroutine = null;
    }
}
