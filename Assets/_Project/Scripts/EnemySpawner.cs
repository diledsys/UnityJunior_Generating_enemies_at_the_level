using System.Collections;
using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private float _spawnInterval = 2f;

    private WaitForSeconds _wait;
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(_spawnInterval);
    }

    private void OnEnable()
    {
        StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    public void StartSpawning()
    {
        if (_spawnCoroutine != null)
            return;

        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnCoroutine == null)
            return;

        StopCoroutine(_spawnCoroutine);
        _spawnCoroutine = null;
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return _wait;
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        if (_enemyPool == null || _spawnPoints == null || _spawnPoints.Length == 0)
            return;

        int index = Random.Range(0, _spawnPoints.Length);
        SpawnPoint spawnPoint = _spawnPoints[index];

        EnemyMover enemy = _enemyPool.Get(spawnPoint.Position, Quaternion.identity);
        enemy.SetDirection(spawnPoint.Direction);

        ReturnToPoolAfterTime lifetime = enemy.GetComponent<ReturnToPoolAfterTime>();
        if (lifetime != null)
        {
            lifetime.Init(_enemyPool);
            lifetime.StartTimer();
        }
    }
}
