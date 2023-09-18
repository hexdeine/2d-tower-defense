using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeWaypoints();
        }
        else {
            Destroy(this);
        }
    }
    private static EnemySpawner _instance;
    public static EnemySpawner Instance {
        get { return _instance; }
    }

    public int enemyMaxSpawnCount = 20;
    public float enemySpawnRate = 2f;
    public float enemyTravelDuration = 20f;
    public Transform waypoints;
    public GameObject enemyPrefab;

    private List<Transform> Waypoints;
    private int enemiesSpawned = 0;
    private GameObject enemyParent;
    private bool canSpawnEnemy = true;
    private float timer = 0f;

    public List<Transform> getWaypoints() {
        return Waypoints;
    }

    private void Update() {
        if (Time.time < timer + 1f / enemySpawnRate) return;

        SpawnEnemy();

        timer = Time.time;
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
        Debug.Log(enemiesSpawned);
    }
}
