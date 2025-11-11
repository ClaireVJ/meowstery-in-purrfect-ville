using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoEvents
{
    public event Action OnPlayerTakePhoto;
    public void PlayerTakePhoto()
    {
        OnPlayerTakePhoto?.Invoke();
    }

    public event Action<Texture2D> OnNewPhotoDisplayStarted;
    public void DisplayNewPhotoInUI(Texture2D photoTexture)
    {
        OnNewPhotoDisplayStarted?.Invoke(photoTexture);
    }

    public event Action OnNewPhotoDeleted;
    public void DeleteNewPhoto()
    {
        OnNewPhotoDeleted?.Invoke();
    }

    public event Action OnNewPhotoSaved;
    public void SaveNewPhoto()
    {
        OnNewPhotoSaved?.Invoke();
    }

    public event Action OnNewPhotoDisplayFinished;
    public void HideNewPhotoInUI()
    {
        OnNewPhotoDisplayFinished?.Invoke();
    }
}
