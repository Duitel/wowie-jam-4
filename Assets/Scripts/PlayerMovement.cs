using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float turnSpeed = 300f;
    [SerializeField] private float offsetAngleToAlignWithCamera = 225;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
