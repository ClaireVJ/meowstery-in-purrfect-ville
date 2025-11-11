using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    // Called in InputManager's Interact functions
    public void Interact()
    {
        if (PlayerManager.instance.GetCurrentPlayerState() == PlayerState.NORMAL)
        {
            GameEventManager.instance.interactEvents.OnInteract();
        }
        else if (PlayerManager.instance.GetCurrentPlayerState() == PlayerState.INTERACTING)
        {
            GameEventManager.instance.interactEvents.ContinueDialogue();
        }
    }
}
