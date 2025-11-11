using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        // Update the orientation based on the camera's pos 
        Vector3 viewDirection = playerTransform.position - new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;
    }
}
