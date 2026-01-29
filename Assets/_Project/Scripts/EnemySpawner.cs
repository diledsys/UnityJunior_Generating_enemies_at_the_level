using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private EnemyMover _enemyPrefab;
    [SerializeField] private float _spawnInterval = 2f;

    private Coroutine _spawnRoutine;
    private bool _isRunning;

    private void OnEnable()
    {
        StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    private void StartSpawning()
    {
        if (_isRunning)
            return;

        _isRunning = true;
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private void StopSpawning()
    {
        _isRunning = false;
        _spawnRoutine = null;
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isRunning)
        {
            yield return new WaitForSeconds(_spawnInterval);
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        if (_enemyPrefab == null || _spawnPoints == null || _spawnPoints.Length == 0)
            return;

        int index = Random.Range(0, _spawnPoints.Length);
        SpawnPoint sp = _spawnPoints[index];

        EnemyMover enemy = Instantiate(_enemyPrefab, sp.Position, Quaternion.identity);
        enemy.SetMoveDirection(sp.MoveDirection);
    }
}
