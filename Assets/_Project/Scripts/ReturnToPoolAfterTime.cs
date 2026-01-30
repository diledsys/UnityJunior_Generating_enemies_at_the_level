using System.Collections;
using UnityEngine;

public sealed class ReturnToPoolAfterTime : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;

    private EnemyPool _pool;
    private EnemyMover _enemy;
    private Coroutine _routine;

    private void Awake()
    {
        _enemy = GetComponent<EnemyMover>();
    }

    private void OnDisable()
    {
        StopTimer();
    }

    public void Init(EnemyPool pool)
    {
        _pool = pool;
    }

    public void StartTimer()
    {
        StopTimer();
        _routine = StartCoroutine(TimerRoutine());
    }

    private void StopTimer()
    {
        if (_routine == null)
            return;

        StopCoroutine(_routine);
        _routine = null;
    }

    private IEnumerator TimerRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);

        if (_pool != null && _enemy != null)
            _pool.Release(_enemy);
    }
}
