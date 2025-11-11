using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float jumpCoolDown;
    [SerializeField] private bool canJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        canJump = true;
    }

    // Called in InputManager's Jump function
    public void Jump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f, groundMask);

        if (canJump && isGrounded)
        {
            canJump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(JumpCoolDown());
        }
    }

    IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(jumpCoolDown);
        canJump = true;
    }
}
