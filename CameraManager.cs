using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject thirdPersonCineMachine;
    [SerializeField] private ThirdPersonCamera thirdPersonCamera;

    [SerializeField] private CamPosition camPosition;
    [SerializeField] private PhotoModeCamera photoModeCamera;

    private void OnEnable()
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.OnPlayerStateChanged += OnPlayerStateChange;
        }
    }

    private void OnDisable()
    {
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.OnPlayerStateChanged -= OnPlayerStateChange;
        }
    }

    private void OnPlayerStateChange(PlayerState newPlayerState)
    {
        // Set all things related to camera to false
        thirdPersonCineMachine.SetActive(false);
        thirdPersonCamera.enabled = false;

        camPosition.enabled = false;
        photoModeCamera.enabled = false;

        switch (newPlayerState)
        {
            case PlayerState.NORMAL:
                IsCursorConfined(false);
                thirdPersonCineMachine.SetActive(true);
                thirdPersonCamera.enabled = true;
                break;

            case PlayerState.INTERACTING:
                IsCursorConfined(true);
                thirdPersonCamera.enabled = true;
                break;

            case PlayerState.PHOTOTAKING:
                IsCursorConfined(false);
                camPosition.enabled = true;
                photoModeCamera.enabled = true;
                break;

            case PlayerState.UI:
                IsCursorConfined(true);
                camPosition.enabled = true;
                photoModeCamera.enabled = true;
                break;

            default:
                Debug.LogWarning("Out of bound state");
                break;
        }
    }

    private void IsCursorConfined(bool isTrue)
    {
        if (isTrue == true)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
