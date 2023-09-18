using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this);
        }
    }
    private static ObjectPool _instance;
    public static ObjectPool Instance {
        get { return _instance; }
    }

    private GameObject objectToPool;
    public List<GameObject> objectPoolList;
    private Transform objectPoolHolder;    

    private void OnEnable() {
        objectPoolList = new List<GameObject>();
    }

    private void Start() {
        objectPoolHolder = GameObject.FindGameObjectWithTag("Object Pool").GetComponent<Transform>();
    }

    public GameObject PoolObject(GameObject _object, Vector2 pos) {
        objectToPool = _object;

        if (objectPoolList.Count > 0) {
            for (int i = 0; i < objectPoolList.Count; i++) {
                if (!objectPoolList[i].activeInHierarchy && objectPoolList[i].name.StartsWith(objectToPool.name)) {
                    objectPoolList[i].transform.position = pos;
                    return objectPoolList[i];
                }
            }
        }

        GameObject obj = Instantiate(objectToPool, pos, Quaternion.identity);
        obj.name = objectToPool.name + "_" + (objectPoolList.Count + 1) + "_Pooled";
        obj.transform.SetParent(objectPoolHolder);
        obj.SetActive(false);
        objectPoolList.Add(obj);

        return obj;
    }

    
}
