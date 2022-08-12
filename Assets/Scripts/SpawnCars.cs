using System;
using System.Collections.Generic;
using UnityEngine;

class SpawnCars : SpawnObject
{
    [Header("Attributes for car")]
    [SerializeField] GameObject checkPointsParent;

    protected override void DoAfterSpawn(GameObject spawnedObject)
    {
        MoveToCheckpoint moveToCheckpoint = spawnedObject.GetComponent<MoveToCheckpoint>();
        moveToCheckpoint.checkPointPathsParent = checkPointsParent;
        moveToCheckpoint.Initialize();
    }
}
