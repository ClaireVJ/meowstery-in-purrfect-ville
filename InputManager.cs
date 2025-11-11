using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerInteract playerInteract;
    private PlayerTakePhoto playerPhotoMode;
    [SerializeField] private PhotoModeCamera photoModeCamera;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerInteract = GetComponent<PlayerInteract>();
        playerPhotoMode = GetComponent<PlayerTakePhoto>();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.Basic.HorizontalMove.performed += OnHorizontalMovePerformed;
        playerInputActions.Basic.HorizontalMove.canceled += OnHorizontalMoveCanceled;

        playerInputActions.Basic.VerticalMove.performed += OnVerticalMovePerformed;
        playerInputActions.Basic.VerticalMove.canceled += OnVerticalMoveCanceled;

        playerInputActions.Basic.Sprint.performed += OnSprintPerformed;
        playerInputActions.Basic.Sprint.canceled += OnSprintCanceled;

        playerInputActions.Basic.Jump.performed += OnJumpPerformed;

        playerInputActions.PhotoMode.HorizontalLook.performed += OnHorizontalLookPerformed;
        playerInputActions.PhotoMode.HorizontalLook.canceled += OnHorizontalLookCanceled;

        playerInputActions.PhotoMode.VerticalLook.performed += OnVerticalLookPerformed;
        playerInputActions.PhotoMode.VerticalLook.canceled += OnVerticalLookCanceled;

        playerInputActions.Basic.StartInteraction.performed += OnInteractPerformed;
        playerInputActions.Dialogue.Continue.performed += OnInteractPerformed;

        playerInputActions.Basic.EnterPhotoMode.performed += OnEnterPhotoModePerformed;
        playerInputActions.PhotoMode.ExitPhotoMode.performed += OnExitPhotoModePerformed;
        playerInputActions.PhotoMode.TakePhoto.performed += OnTakePhotoPerformed;

        playerInputActions.Basic.EnterNotebook.performed += OnEnterNotebookPerformed;
        playerInputActions.UI.ExitNotebook.performed += OnExitNotebookPerformed;

        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.OnPlayerStateChanged += OnPlayerStateChange;
        }
    }

    private void OnDisable()
    {
        playerInputActions.Basic.HorizontalMove.performed -= OnHorizontalMovePerformed;
        playerInputActions.Basic.HorizontalMove.canceled -= OnHorizontalMoveCanceled;

        playerInputActions.Basic.VerticalMove.performed -= OnVerticalMovePerformed;
        playerInputActions.Basic.VerticalMove.canceled -= OnVerticalMoveCanceled;

        playerInputActions.Basic.Sprint.performed -= OnSprintPerformed;
        playerInputActions.Basic.Sprint.canceled -= OnSprintCanceled;

        playerInputActions.Basic.Jump.performed -= OnJumpPerformed;

        playerInputActions.PhotoMode.HorizontalLook.performed -= OnHorizontalLookPerformed;
        playerInputActions.PhotoMode.HorizontalLook.canceled -= OnHorizontalLookCanceled;

        playerInputActions.PhotoMode.VerticalLook.performed -= OnVerticalLookPerformed;
        playerInputActions.PhotoMode.VerticalLook.canceled -= OnVerticalLookCanceled;

        playerInputActions.Basic.StartInteraction.performed -= OnInteractPerformed;
        playerInputActions.Dialogue.Continue.performed -= OnInteractPerformed;

        playerInputActions.Basic.EnterPhotoMode.performed -= OnEnterPhotoModePerformed;
        playerInputActions.PhotoMode.ExitPhotoMode.performed -= OnExitPhotoModePerformed;
        playerInputActions.PhotoMode.TakePhoto.performed -= OnTakePhotoPerformed;

        playerInputActions.Basic.EnterNotebook.performed -= OnEnterNotebookPerformed;
        playerInputActions.UI.ExitNotebook.performed -= OnExitNotebookPerformed;

        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.OnPlayerStateChanged -= OnPlayerStateChange;
        }

        playerInputActions.Disable();
    }

    private void OnPlayerStateChange(PlayerState newPlayerState)
    {
        playerInputActions.Disable();

        playerMovement.enabled = false;
        playerJump.enabled = false;
        playerInteract.enabled = false;
        playerPhotoMode.enabled = false;

        switch (newPlayerState)
        {
            case PlayerState.NORMAL:
                playerInputActions.Basic.Enable();
                playerMovement.enabled = true;
                playerJump.enabled = true;
                playerInteract.enabled = true;
                break;

            case PlayerState.INTERACTING:
                playerInputActions.Dialogue.Enable();
                playerInteract.enabled = true;
                break;

            case PlayerState.PHOTOTAKING:
                playerInputActions.PhotoMode.Enable();
                playerInputActions.Basic.Enable();
                playerMovement.enabled = true;
                playerPhotoMode.enabled = true;
                break;

            case PlayerState.UI:
                playerInputActions.UI.Enable();
                break;

            default:
                Debug.LogWarning("Switch Case broke");
                break;
        }
    }

    private void OnHorizontalMovePerformed(InputAction.CallbackContext obj)
    {
        float horizontalInput = playerInputActions.Basic.HorizontalMove.ReadValue<float>();
        playerMovement?.SetHorizontalInput(horizontalInput);
    }
    private void OnHorizontalMoveCanceled(InputAction.CallbackContext obj)
    {
        float horizontalInput = 0f;
        playerMovement?.SetHorizontalInput(horizontalInput);
    }

    private void OnVerticalMovePerformed(InputAction.CallbackContext obj)
    {
        float verticalInput = playerInputActions.Basic.VerticalMove.ReadValue<float>();
        playerMovement?.SetVerticalInput(verticalInput);
    }
    private void OnVerticalMoveCanceled(InputAction.CallbackContext obj)
    {
        float verticalInput = 0f;
        playerMovement?.SetVerticalInput(verticalInput);
    }

    private void OnSprintPerformed(InputAction.CallbackContext obj)
    {
        bool isSprint = true;
        playerMovement?.SetIsSprinting(isSprint);
    }
    private void OnSprintCanceled(InputAction.CallbackContext obj)
    {
        bool isSprint = false;
        playerMovement?.SetIsSprinting(isSprint);
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        playerJump?.Jump();
    }

    private void OnHorizontalLookPerformed(InputAction.CallbackContext obj)
    {
        float horizontalInput = playerInputActions.PhotoMode.HorizontalLook.ReadValue<float>();
        photoModeCamera?.SetHorizontalInput(horizontalInput);
    }
    private void OnHorizontalLookCanceled(InputAction.CallbackContext obj)
    {
        float horizontalInput = 0f;
        photoModeCamera?.SetHorizontalInput(horizontalInput);
    }

    private void OnVerticalLookPerformed(InputAction.CallbackContext obj)
    {
        float verticalInput = playerInputActions.PhotoMode.VerticalLook.ReadValue<float>();
        photoModeCamera?.SetVerticalInput(verticalInput);
    }
    private void OnVerticalLookCanceled(InputAction.CallbackContext obj)
    {
        float verticalInput = 0f;
        photoModeCamera?.SetVerticalInput(verticalInput);
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    {
        playerInteract?.Interact();
    }

    private void OnEnterPhotoModePerformed(InputAction.CallbackContext obj)
    {
        PlayerManager.instance.SwitchCurrentPlayerState(PlayerState.PHOTOTAKING);
    }
    private void OnExitPhotoModePerformed(InputAction.CallbackContext obj)
    {
        PlayerManager.instance.SwitchCurrentPlayerState(PlayerState.NORMAL);
    }

    private void OnTakePhotoPerformed(InputAction.CallbackContext obj)
    {
        playerPhotoMode?.TakePhotoInput();
    }

    private void OnEnterNotebookPerformed(InputAction.CallbackContext obj)
    {
        GameEventManager.instance.notebookEvents.OpenNotebook();
    }

    private void OnExitNotebookPerformed(InputAction.CallbackContext obj)
    {
        GameEventManager.instance.notebookEvents.CloseNotebook();
    }
}
