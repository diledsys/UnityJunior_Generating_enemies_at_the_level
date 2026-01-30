using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyPool : MonoBehaviour
{
    [SerializeField] private EnemyMover _prefab;
    [SerializeField] private int _prewarmCount = 10;

    private readonly Queue<EnemyMover> _pool = new();

    private void Awake()
    {
        for (int i = 0; i < _prewarmCount; i++)
        {
            EnemyMover enemy = CreateNew();
            Release(enemy);
        }
    }

    public EnemyMover Get(Vector3 position, Quaternion rotation)
    {
        EnemyMover enemy = _pool.Count > 0 ? _pool.Dequeue() : CreateNew();
        enemy.transform.SetPositionAndRotation(position, rotation);
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    public void Release(EnemyMover enemy)
    {
        enemy.StopMoving();
        enemy.gameObject.SetActive(false);
        _pool.Enqueue(enemy);
    }

    private EnemyMover CreateNew()
    {
        EnemyMover enemy = Instantiate(_prefab, transform);
        enemy.gameObject.SetActive(false);
        return enemy;
    }
}
