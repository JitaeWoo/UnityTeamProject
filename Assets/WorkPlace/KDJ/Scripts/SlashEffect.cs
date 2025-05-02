using System.Collections;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    [SerializeField] private GameObject _slashEffect;

    private GameObject _player;
    private Coroutine _slashCoroutine;
    private WaitForSeconds _wait = new WaitForSeconds(0.02f);

    private void Start()
    {
        _player = Manager.Player.Player;
        if (_slashCoroutine == null)
            _slashCoroutine = StartCoroutine(RemoveSlash());
    }

    IEnumerator RemoveSlash()
    {
        yield return _wait;
        _slashEffect.transform.position = new Vector3(_player.transform.position.x, 1, _player.transform.position.z);
        yield return _wait;
        _slashEffect.transform.position = new Vector3(_player.transform.position.x, 1, _player.transform.position.z);
        yield return _wait;
        _slashEffect.transform.position = new Vector3(_player.transform.position.x, 1, _player.transform.position.z);
        yield return _wait;
        _slashEffect.transform.position = new Vector3(_player.transform.position.x, 1, _player.transform.position.z);
        yield return _wait;
        _slashEffect.transform.position = new Vector3(_player.transform.position.x, 1, _player.transform.position.z);

        Destroy(_slashEffect.gameObject);
        _slashCoroutine = null;
    }
}
