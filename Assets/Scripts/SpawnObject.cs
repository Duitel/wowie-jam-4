using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float startTime;
    [SerializeField] private float timeBetween;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), startTime, timeBetween);
    }

    void Spawn()
    {
        GameObject spawnedObject = Instantiate(objectToSpawn);
        spawnedObject.transform.SetParent(transform);

        DoAfterSpawn(spawnedObject);
    }

    protected virtual void DoAfterSpawn(GameObject spawnedObject)
    {
        // Can be overwritten in child
    }
}
