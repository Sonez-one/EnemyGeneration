using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private Enemy _enemyPrefab;

    private readonly float _spawnRate = 2f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            int randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count);
            Vector3 direction = Random.insideUnitSphere.normalized;
            
            var spawnedEnemy = Instantiate(_enemyPrefab, _spawnPoints[randomSpawnPointIndex].transform.position, Quaternion.identity);

            spawnedEnemy.Init(direction);

            yield return new WaitForSeconds(_spawnRate);
        }
    }
}
