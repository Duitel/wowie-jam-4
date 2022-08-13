using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupTrafficParticipant : MonoBehaviour
{
    public GameObject checkPointPathsParent;
    private Transform target;
    private List<Transform> targets;
    private int targetIndex = 0;

    [Header("Movement sttings")]
    private float speed;
    public float maxSpeed;
    public float minSpeed;
    public float stopInBetweenDistance;

    private MoveToTarget moveToTarget;

    public void Setup()
    {
        stopInBetweenDistance = Random.Range(0.6f, 0.9f);
        speed = Random.Range(minSpeed, maxSpeed);

        targets = checkPointPathsParent.GetComponent<CheckPointPaths>().GetRandomPath();
        target = targets[targetIndex];

        transform.position = target.position;
        transform.rotation = target.rotation;
        transform.Rotate(new Vector3(0, 90, 0));

        moveToTarget = gameObject.GetComponent<MoveToTarget>();
        moveToTarget.stopInBetweenDistance = stopInBetweenDistance;
        moveToTarget.speed = speed;
        moveToTarget.targets = targets;
        moveToTarget.target = target;
        moveToTarget.targetIndex = targetIndex;

        Invoke(nameof(StartCarMovement), 0.5f);
    }

    private void StartCarMovement()
    {
        moveToTarget.enabled = true;
    }
}
