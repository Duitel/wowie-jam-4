using System;
using System.Collections.Generic;
using UnityEngine;

class SpawnCars : SpawnObject
{
    [Header("Attributes for car")]
    [SerializeField] GameObject checkPointsParent;

    protected override void DoAfterSpawn(GameObject spawnedObject)
    {
        SetupTrafficParticipant setupCar= spawnedObject.GetComponent<SetupTrafficParticipant>();
        setupCar.checkPointPathsParent = checkPointsParent;
        setupCar.Setup();
    }
}
