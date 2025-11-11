using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhotoModeCamera : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    [Header("Variables")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private float xClampLimit;

    private float xRotation;
    private float yRotation;

    private float horizontalInput;
    private float verticalInput;

    // Update is called once per frame
    void Update()
    {
        //Get mouse input * by seconds * by the sensivity set
        float mouseX = horizontalInput * Time.deltaTime * sensX;
        float mouseY = verticalInput * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        //Limits vertical looking to a certain degrees up and down
        xRotation = Mathf.Clamp(xRotation, -xClampLimit, xClampLimit);

        //Rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void SetHorizontalInput(float horizontalLookInput)
    {
        horizontalInput = horizontalLookInput;
    }
    public void SetVerticalInput(float verticalLookInput)
    {
        verticalInput = verticalLookInput;
    }
}
