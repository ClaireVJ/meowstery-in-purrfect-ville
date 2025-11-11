using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakePhoto : MonoBehaviour
{
    // Called in InputManager's Photo function
    public void TakePhotoInput()
    {
        GameEventManager.instance.photoEvents.PlayerTakePhoto();
    }
}