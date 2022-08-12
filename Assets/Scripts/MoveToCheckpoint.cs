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

    private Vector3 debugMove;
    private Vector3 debugTurnGap;

    public void Initialize()
    {
        controller = gameObject.AddComponent<CharacterController>();

        checkPointController = checkPointPathsParent.GetComponent<CheckPointPaths>();
        targets = checkPointController.GetRandomPath();
        SelectNextTarget();

        transform.position = target.position;
        transform.rotation = target.rotation;
        transform.Rotate(new Vector3(0,90,0));

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

        Vector3 move = target.position - transform.position;
        if (move.magnitude < targetReachedBoundary)
        {
            Debug.Log("Within reach: Magnitude " + move.magnitude);
            DoOnTargetReached();
            return;
        }

        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = turnSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

        controller.Move(transform.forward * Time.deltaTime * speed);
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
