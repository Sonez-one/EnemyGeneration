using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Target _targetPrefab;
    [SerializeField] private List<WayPoint> _wayPoints;

    private Target _target;

    private void Start()
    {
        IdentifyTargetForEnemies();
    }

    public Enemy CreateEnemy()
    {
        Enemy spawnedEnemy = Instantiate(_enemyPrefab);

        spawnedEnemy.Init(transform.position, _target);

        return spawnedEnemy;
    }

    private void IdentifyTargetForEnemies()
    {
        _target = Instantiate(_targetPrefab);

        _target.Init(transform.position, _wayPoints);
    }
}
