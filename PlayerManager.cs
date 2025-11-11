using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Singleton pattern
    private static PlayerManager playerInstance;
    public static PlayerManager instance 
    {
        get
        {
            if (playerInstance == null)
            {
                playerInstance = GameObject.FindAnyObjectByType<PlayerManager>();
            }
            return playerInstance;
        }
        private set
        {
            playerInstance = value;
        }
    }

    private PlayerState currentPlayerState;
    private PlayerState previousPlayerState;

    // Getter method - Called in other scripts to get the current player state
    public PlayerState GetCurrentPlayerState()
    {
        return currentPlayerState;
    }

    // Getter method - Called in other scripts to get the previous player state
    public PlayerState GetPreviousPlayerState()
    {
        return previousPlayerState;
    }

    private void Start()
    {
        SwitchCurrentPlayerState(PlayerState.NORMAL);
    }

    // Sends out an event when the playerState is changed. Allows other scripts to switch their functions based on current player state
    public event Action<PlayerState> OnPlayerStateChanged;
    public void SwitchCurrentPlayerState(PlayerState newPlayerState)
    {
        OnPlayerStateChanged?.Invoke(newPlayerState);

        previousPlayerState = currentPlayerState;
        currentPlayerState = newPlayerState;
    }
}
