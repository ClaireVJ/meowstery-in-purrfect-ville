using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPhotoChoiceButton : MonoBehaviour
{
    public void SavePhotoButton()
    {
        GameEventManager.instance.photoEvents.SaveNewPhoto();
    }

    public void DeletePhotoButton()
    {
        GameEventManager.instance.photoEvents.DeleteNewPhoto();
    }
}
