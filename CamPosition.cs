using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    [SerializeField] private Transform camPos;

    // Update is called once per frame
    void LateUpdate()
    {
        // Set the Camera's position to the camPos position (which is in the player)
        transform.position = camPos.position;
    }
}
