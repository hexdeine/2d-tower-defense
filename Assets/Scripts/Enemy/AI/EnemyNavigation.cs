using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigation : MonoBehaviour
{
    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this);
        }

        Waypoints = EnemySpawner.Instance.getWaypoints();
    }
    private static EnemyNavigation _instance;
    public static EnemyNavigation Instance {
        get { return _instance; }
    }

    private List<Transform> Waypoints;

    public float getTotalPathLength() {
        float totalLength = 0;

        if (Waypoints != null) {
            for (int i = 0; i < Waypoints.Count - 1; i++) {
                totalLength += Vector2.Distance(Waypoints[i].transform.position, Waypoints[i + 1].transform.position);
            }
        }

        return totalLength;
    }
}
