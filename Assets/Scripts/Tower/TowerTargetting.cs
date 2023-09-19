using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class TowerTargetting : MonoBehaviour
{
    private float maxTargetRange = 2f;
    private float turnSpeed = 2f;
    private float targetSwitchRate = 0.5f;
    private GameObject target;
    private GameObject[] enemies;
    private float currentTime;

    private void Update() {
        if (Time.time > currentTime * targetSwitchRate) {
            LocateEnemies();
            currentTime = Time.time;
        }
        RotateTurret();
    }

    private void LocateEnemies() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies) {
            float targetDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxTargetRange) {
                target = enemy;
            }
        }
    }

    private void RotateTurret() {
        if (target == null) return;
        if (Vector2.Distance(transform.position, target.transform.position) > maxTargetRange) return;

        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion debugrotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        //debugrotation.Normalize();
        transform.rotation = debugrotation;
    }
}
