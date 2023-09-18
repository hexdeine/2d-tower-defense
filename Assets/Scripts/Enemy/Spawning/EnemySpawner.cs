using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemyMaxSpawnCount = 2;
    public int enemySpawnRate = 1;
    public Transform waypoints;
    public GameObject enemyPrefab;

    private List<Transform> Waypoints;
    private int enemiesSpawned = 0;
    private GameObject enemyParent;
    private bool canSpawnEnemy = true;

    private void Awake() {
        InitializeWaypoints();
    }

    private void Update() {
        SpawnEnemy();
    }

    private void InitializeWaypoints() {
        Waypoints = new List<Transform>();

        foreach (Transform child in waypoints) {
            Waypoints.Add(child);
        }
    }

    private void SpawnEnemy() {
        if (enemyPrefab == null) return;

        if (!canSpawnEnemy) return;

        GameObject enemy = ObjectPool.Instance.PoolObject(enemyPrefab, Waypoints[0].position);
        enemy.SetActive(true);

        enemiesSpawned++;

        if (enemiesSpawned >= enemyMaxSpawnCount) {
            canSpawnEnemy = false;
        }
    }
}
