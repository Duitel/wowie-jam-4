using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectToSpawn;
    [SerializeField] private float startTime;
    [SerializeField] private float timeBetween;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), startTime, timeBetween);
    }

    void Spawn()
    {
        int randomObjectIndex = Random.Range(0, objectToSpawn.Count);
        GameObject spawnedObject = Instantiate(objectToSpawn[randomObjectIndex]);
        spawnedObject.transform.SetParent(transform);

        DoAfterSpawn(spawnedObject);
    }

    protected virtual void DoAfterSpawn(GameObject spawnedObject)
    {
        // Can be overwritten in child
    }
}
