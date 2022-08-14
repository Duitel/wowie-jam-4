using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public List<Transform> targets;
    public int targetIndex = 0;
    [SerializeField] private float targetReachedBoundary = 2f;

    [Header("Movement settings")]
    private CharacterController controller;
    public float speed;
    [SerializeField] private float turnSpeed = 300f;    
    public float stopInBetweenDistance;

    private Animator animator;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position + new Vector3(Random.Range(-1f, -1f), 0, Random.Range(1f,-1f)) * 1.7f;
        
        if (targetDirection.magnitude < targetReachedBoundary)
        {
            DoOnTargetReached();
            return;
        }

        if (ObstructionByObstacle())
        {
            if(animator) animator.SetFloat("speed", 0);
            return;
        }

        if (animator) animator.SetFloat("speed", speed);

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

    private bool ObstructionByObstacle()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, stopInBetweenDistance))
        {
            print("There is something in front of the object!");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * stopInBetweenDistance, Color.yellow);
            return true;
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * stopInBetweenDistance, Color.white);
        return false;
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
