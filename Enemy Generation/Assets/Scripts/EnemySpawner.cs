using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private readonly float _spawnRate = 1.5f;
    private readonly int _poolCapacity = 7;
    private readonly int _poolMaxSize = 7;

    private int _currenSpawnPointIndex;

    private ObjectPool<Enemy> _enemies;

    private void Awake()
    {
        _enemies = new ObjectPool<Enemy>(
            createFunc: () => Create(),
            actionOnGet: (enemy) => ActionOnGet(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => ActionOnDestroy(enemy),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private Enemy Create()
    {
        Enemy enemy = RandomiseSpawnPoint().CreateEnemy();

        enemy.ReachedTarget += Release;

        return enemy;
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.transform.position = enemy.StartPosition;

        enemy.gameObject.SetActive(true);

        StartCoroutine(enemy.Move());
    }

    private void ActionOnDestroy(Enemy enemy)
    {
        StopCoroutine(enemy.Move());

        enemy.ReachedTarget -= Release;

        Destroy(enemy.gameObject);
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_spawnRate);

        while (enabled)
        {
            yield return wait;

            GetEnemy();
        }
    }

    private void GetEnemy()
    {
        _enemies.Get();
    }

    private void Release(Enemy enemy)
    {
        _enemies.Release(enemy);
    }

    private SpawnPoint RandomiseSpawnPoint()
    {
        _currenSpawnPointIndex = Random.Range(0, _spawnPoints.Count);
        SpawnPoint currentSpawnPoint = _spawnPoints[_currenSpawnPointIndex];

        return currentSpawnPoint;
    }
}
