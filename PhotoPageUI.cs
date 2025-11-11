using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoPageUI : MonoBehaviour
{
    [SerializeField] private List<Texture2D> photoTextures;
    [SerializeField] private List<string> photoFilePaths;
    [SerializeField] private int currentPhotoIndex;

    [SerializeField] private RawImage photoDisplay;

    private void OnEnable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.notebookEvents.OnCloseTab += CloseTab;
            GameEventManager.instance.notebookEvents.OnPhotoPageOpen += LoadAllPhotos;
            GameEventManager.instance.notebookEvents.OnPhotoForwardButtonPressed += IncreasePhotoIndex;
            GameEventManager.instance.notebookEvents.OnPhotoBackButtonPressed += DecreasePhotoIndex;
            GameEventManager.instance.notebookEvents.OnPhotoDeleteButtonPressed += DeletePhoto;
        }
    }

    private void OnDisable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.notebookEvents.OnCloseTab -= CloseTab;
            GameEventManager.instance.notebookEvents.OnPhotoPageOpen -= LoadAllPhotos;
            GameEventManager.instance.notebookEvents.OnPhotoForwardButtonPressed -= IncreasePhotoIndex;
            GameEventManager.instance.notebookEvents.OnPhotoBackButtonPressed -= DecreasePhotoIndex;
            GameEventManager.instance.notebookEvents.OnPhotoDeleteButtonPressed -= DeletePhoto;
        }
    }

    private void LoadAllPhotos()
    {
        // Create new Lists
        photoTextures = new List<Texture2D>();
        photoFilePaths = new List<string>();

        // Find the path to the "Photos" folder
        string filepath = Path.Combine(Application.persistentDataPath, "Photos");

        // Returns the names of files that meet specified criteria (pngFiles are from the "Photos" folder and are PNG's)
        string[] pngFiles = Directory.GetFiles(filepath, "*.png");

        // For every PNG found in the "Photos" folder...
        foreach (string pngFile in pngFiles)
        {
            // Final check to make sure the PNG exists
            if (File.Exists(pngFile))
            {
                // File.ReadAllBytes - Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
                byte[] pngBytes = File.ReadAllBytes(pngFile);

                // Create a temporary texture that will be resized when the code loads the image
                Texture2D loadedTexture = new Texture2D(1, 1);

                // ImageConversion.LoadImage - Loads PNG, JPG or EXR image byte array into a texture.
                // Puts the byte array into the texture2D
                if (ImageConversion.LoadImage(loadedTexture, pngBytes))
                {
                    // Add the loadedTexture to the photoTexture list
                    photoTextures.Add(loadedTexture);

                    // Add the directory path to the photoFilePaths list
                    photoFilePaths.Add(pngFile);
                }
                else
                {
                    Debug.Log("Failed to load image!");
                }
            }
            else
            {
                Debug.Log("File doesn't exists");
            }
        }

        currentPhotoIndex = 0;
        DisplayPhoto();
    }

    private void DisplayPhoto()
    {
        if (photoTextures.Count == 0)
        {
            photoDisplay.texture = null;
        }
        else
        {
            // Set the raw image (photoDisplay) to the one of the textures in the photoTexture list
            photoDisplay.texture = photoTextures[currentPhotoIndex];
        }
    }

    // Deletes the photo currently displayed
    private void DeletePhoto()
    {
        // Get the path to the current Photo displayed 
        string photoToDelete = photoFilePaths[currentPhotoIndex];

        //Delete the photo
        if (File.Exists(photoToDelete))
        {
            File.Delete(photoToDelete);
        }
        else
        {
            Debug.LogWarning("No file found to delete");
        }

        // Remove the texture from the list
        photoFilePaths.Remove(photoToDelete);
        photoTextures.Remove(photoTextures[currentPhotoIndex]);

        // Move forward +1 in list
        IncreasePhotoIndex();
    }

    private void DecreasePhotoIndex()
    {
        // Substract 1 from currentPhotoIndex
        currentPhotoIndex--;

        // If the currentPhotoIndex is less than 0
        if (currentPhotoIndex < 0)
        {
            // Set the currentPhotoIndex to the max element in photoTextures
            currentPhotoIndex = photoTextures.Count - 1;
        }

        DisplayPhoto();
    }

    private void IncreasePhotoIndex()
    {
        // Add 1 to currentPhotoIndex
        currentPhotoIndex++;

        // If the currentPhotoIndex equals to the amount of 
        if (currentPhotoIndex == photoTextures.Count)
        {
            currentPhotoIndex = 0;
        }

        DisplayPhoto();
    }


    private void CloseTab()
    {
        // Empty the lists and set the RawImage's texture to nothing
        photoTextures.Clear();

        if (photoDisplay.texture != null)
        {
            photoDisplay.texture = null;
        }

        photoFilePaths.Clear();
    }
}
