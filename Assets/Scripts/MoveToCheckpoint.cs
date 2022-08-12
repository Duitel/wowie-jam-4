using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCheckpoint : MonoBehaviour
{
    private bool initialized = false;

    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float turnSpeed = 300f;
    [SerializeField] private float offsetAngleToAlignWithCamera = 225;

    public GameObject checkPointPathsParent;
    private CheckPointPaths checkPointController;
    private List<Transform> targets;
    private Transform target;
    private int targetIndex = -1;
    [SerializeField] private float targetReachedBoundary = 2f;

    public void Initialize()
    {
        controller = gameObject.AddComponent<CharacterController>();

        checkPointController = checkPointPathsParent.GetComponent<CheckPointPaths>();
        targets = checkPointController.GetRandomPath();
        SelectNextTarget();
        transform.position = target.position;

        Invoke(nameof(SetInitialized), 0.5f);
    }

    private void SetInitialized()
    {
        initialized = true;
    }

    void Update()
    {
        if (!initialized)
        {
            return;
        }

        Vector3 move = transform.position - target.position;
        if (move.magnitude < targetReachedBoundary)
        {
            DoOnTargetReached();
            return;
        }
        move = Quaternion.Euler(0, offsetAngleToAlignWithCamera, 0) * move;
        move.y = 0;

        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            Vector3 turnGap = (move - gameObject.transform.forward);
            Vector3 turnBy = turnGap.normalized * turnSpeed * Time.deltaTime;
            if (turnGap.magnitude < turnBy.magnitude)
            {
                turnBy = turnGap;
            }

            gameObject.transform.forward += turnBy;
        }

        playerVelocity.y = 0;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void DoOnTargetReached()
    {
        if(targetIndex >= targets.Count - 1)
        {
            Destroy(gameObject);
            return;
        }

        SelectNextTarget();
    }

    private void SelectNextTarget()
    {
        targetIndex++;
        target = targets[targetIndex];
    }
}
