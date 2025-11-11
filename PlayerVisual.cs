using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private float rotationSpeed;

    private void Awake()
    {
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // If the player's moveDirection isn't zero
        if (playerMovement.GetMoveDirection() != Vector3.zero)
        {
            // Change the player's forward to the current moveDirection's forward overtime
            transform.forward = Vector3.Slerp(transform.forward, playerMovement.GetMoveDirection().normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
