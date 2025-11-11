using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTakingManager : MonoBehaviour
{
    [SerializeField] private Camera photoRender;
    [SerializeField] private Texture2D photoTexture;

    [SerializeField] private AudioClip photoClickSFX;

    private void OnEnable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.photoEvents.OnPlayerTakePhoto += TakeNewPhoto;
            GameEventManager.instance.photoEvents.OnNewPhotoSaved += SavePhoto;
            GameEventManager.instance.photoEvents.OnNewPhotoDeleted += DeletePhoto;
        }
    }

    private void OnDisable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.photoEvents.OnPlayerTakePhoto -= TakeNewPhoto;
            GameEventManager.instance.photoEvents.OnNewPhotoSaved -= SavePhoto;
            GameEventManager.instance.photoEvents.OnNewPhotoDeleted -= DeletePhoto;
        }
    }

    private void TakeNewPhoto()
    {
        StartCoroutine(CaptureNewPhoto());
    }

    // Get a screenshot
    IEnumerator CaptureNewPhoto()
    {
        yield return new WaitForEndOfFrame();

        // GetTemporary : Allocate a temporary render texture (create a render texture)
        RenderTexture renderTexture = RenderTexture.GetTemporary(800, 800, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, 1);

        // Disable the Camera component (prevents blurry images)
        photoRender.enabled = false;

        // Set the targetTexture (which takes a renderTexture) to the newly created temporary renderTexture
        photoRender.targetTexture = renderTexture;

        // Manually render the camera (single frame)
        photoRender.Render();

        // Set currently active render texture to the photoRender's texture
        RenderTexture.active = photoRender.targetTexture;

        // Create an image using the render texture
        photoTexture = new Texture2D(800, 800, TextureFormat.RGB24, false);
        photoTexture.ReadPixels(new Rect(0, 0, 800, 800), 0, 0);
        photoTexture.Apply();

        RenderTexture.ReleaseTemporary(renderTexture);

        PlayerManager.instance.SwitchCurrentPlayerState(PlayerState.UI);

        // Call an event to display the new photo
        GameEventManager.instance.photoEvents.DisplayNewPhotoInUI(photoTexture);
    }

    private void SavePhoto()
    {
        // Create a name for the image (png) based on the current time
        string photoName = "Photo" + System.DateTime.Now.ToString("yy-M-dd-m-ss") + ".png";

        // Create a path to the "Photos" folder
        // Path.Combine is used to safely and correctly concatenate (link things together in a chain or series) two or more path strings.
        string photoFolderPath = Path.Combine(Application.persistentDataPath, "Photos");

        // If the "Photos" folder doesn't exist, create it
        if (!Directory.Exists(photoFolderPath))
        {
            // Creates directories (and subdirectories) in the specified path unless they already exist.
            Directory.CreateDirectory(photoFolderPath);
        }

        // Create a path for the photo to the file
        string photoPath = Path.Combine(photoFolderPath, photoName);

        // Encode the texture into PNG bytes
        byte[] photoPngBytes = photoTexture.EncodeToPNG();

        // Write the bytes into the file
        File.WriteAllBytes(photoPath, photoPngBytes);

        Debug.Log("PNG save to : " + photoPath);

        GameEventManager.instance.photoEvents.HideNewPhotoInUI();
        PlayerManager.instance.SwitchCurrentPlayerState(PlayerManager.instance.GetPreviousPlayerState());
    }

    private void DeletePhoto()
    {
        GameEventManager.instance.photoEvents.HideNewPhotoInUI();
        PlayerManager.instance.SwitchCurrentPlayerState(PlayerManager.instance.GetPreviousPlayerState());
    }
}
