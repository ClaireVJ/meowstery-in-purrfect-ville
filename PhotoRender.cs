using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoRender : MonoBehaviour
{
    [SerializeField] private Camera renderCam;

    private void Start()
    {
        renderCam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        // Copy the position, rotation, scale from the player's camera 
        renderCam.CopyFrom(Camera.main);
    }
}
