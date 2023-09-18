using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float startingTime;
    private List<Transform> waypoints;
    private float totalPathDistance;
    private float travelDuration;

    private void Start() {
        startingTime = Time.time;

        waypoints = EnemySpawner.Instance.getWaypoints();
        totalPathDistance = EnemyNavigation.Instance.getTotalPathLength();

        transform.position = waypoints[0].position;
    }

    void Update()
    {
        Move();
    }

    private void Move() {
        float travelTime = Time.time - startingTime;

        travelDuration = EnemySpawner.Instance.enemyTravelDuration;

        if (travelTime <= travelDuration) {

            float distanceTraveled = travelTime / travelDuration * totalPathDistance;

            float currentTotalSegmentLength = 0f;

            for (int i = 0; i < waypoints.Count - 1; i++) {
                float currentSegmentLength = Vector2.Distance(waypoints[i].position, waypoints[i + 1].position);

                if (distanceTraveled < (currentTotalSegmentLength + currentSegmentLength)) {
                    float segmentInterpolation = (distanceTraveled - currentTotalSegmentLength) / currentSegmentLength;
                    transform.position = Vector2.Lerp(waypoints[i].position, waypoints[i + 1].position, segmentInterpolation);
                    break;
                }

                currentTotalSegmentLength += currentSegmentLength;
            }
        }
        else {
            gameObject.SetActive(false);
            startingTime = Time.time;
        }
    }
}
