using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private static int ORIENTATION_CHILD_INDEX = 1;
    private Transform orientation;

    [Header("Variables")]
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    private bool isSprinting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        orientation = transform.GetChild(ORIENTATION_CHILD_INDEX);
    }

    private void Update()
    {
        moveDirection = orientation.right * horizontalInput + orientation.forward * verticalInput;
        SpeedControl();
    }

    private void FixedUpdate()
    {
        if (isSprinting)
        {
            Sprint();
        }
        else
        {
            Walk();
        }
    }

    private void Walk()
    {
        rb.AddForce(moveDirection.normalized * walkSpeed, ForceMode.Impulse);
    }
    private void Sprint()
    {
        rb.AddForce(moveDirection.normalized * sprintSpeed, ForceMode.Impulse);
    }

    private void SpeedControl()
    {
        Vector3 speedVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (!isSprinting && speedVel.magnitude > walkSpeed)
        {
            Vector3 limitedVel = speedVel.normalized * walkSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        else if (isSprinting && speedVel.magnitude > sprintSpeed)
        {
            Vector3 limitedVel = speedVel.normalized * sprintSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    // Called in InputManager's MovementHorizontalInput function
    public void SetHorizontalInput(float horizontalMoveInput)
    {
        horizontalInput = horizontalMoveInput;
    }

    // Called in InputManager's MovementVerticalInput function
    public void SetVerticalInput(float verticalMoveInput)
    {
        verticalInput = verticalMoveInput;
    }

    // Called in InputManager's Sprint function
    public void SetIsSprinting(bool isSprint)
    {
        isSprinting = isSprint;
    }

    // Getter method - Allows other scripts to get the move direction
    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }
}
