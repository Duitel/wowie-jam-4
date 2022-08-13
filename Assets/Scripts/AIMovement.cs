using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] private float playerSpeed = 1.0f;
    [SerializeField] private float turnSpeed = 300f;
    [SerializeField] private float offsetAngleToAlignWithCamera = 225;
    [SerializeField] private Transform target;
    [SerializeField] private float targetReachedBoundary = 2f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = transform.position - target.position;
        if (move.magnitude < targetReachedBoundary)
        {
            return;
        }
        move = Quaternion.Euler(0, offsetAngleToAlignWithCamera, 0) * move;
        move.y = 0;

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            Vector3 turnGap = (move - gameObject.transform.forward);
            Vector3 turnBy = turnGap.normalized * turnSpeed * Time.deltaTime;
            if(turnGap.magnitude < turnBy.magnitude)
            {
                turnBy = turnGap;
            }

            gameObject.transform.forward += turnBy;
        }

        playerVelocity.y = 0;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
