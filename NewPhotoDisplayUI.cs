using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPhotoDisplayUI : MonoBehaviour
{
    [SerializeField] private RawImage newPhotoDisplayUI;
    [SerializeField] private GameObject contentParent;

    private void OnEnable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.photoEvents.OnNewPhotoDisplayStarted += DisplayNewPhoto;
            GameEventManager.instance.photoEvents.OnNewPhotoDisplayFinished += HideNewPhoto;
        }
    }

    private void OnDisable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.photoEvents.OnNewPhotoDisplayStarted -= DisplayNewPhoto;
            GameEventManager.instance.photoEvents.OnNewPhotoDisplayFinished -= HideNewPhoto;
        }
    }

    // Called when OnNewPhotoDisplayStarted is Invoked
    private void DisplayNewPhoto(Texture2D photoTexture)
    {
        contentParent.SetActive(true);
        newPhotoDisplayUI.texture = photoTexture;
    }

    // Called when OnNewPhotoDisplayFinished is Invoked
    private void HideNewPhoto()
    {
        newPhotoDisplayUI.texture = null;
        contentParent.SetActive(false);
    }
}
