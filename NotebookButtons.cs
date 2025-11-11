using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookButtons : MonoBehaviour
{
    public void OpenKeyInfoTab()
    {
        GameEventManager.instance.notebookEvents.OpenTab(TabType.KEYINFORMATION);
    }

    public void OpenTasksTab()
    {
        GameEventManager.instance.notebookEvents.OpenTab(TabType.TASKS);
    }

    public void OpenPhotoTab()
    {
        GameEventManager.instance.notebookEvents.OpenTab(TabType.PHOTODISPLAY);
    }

    public void OpenSettingsTab()
    {
        GameEventManager.instance.notebookEvents.OpenTab(TabType.SETTINGS);
    }

    public void ForwardButtonPressed()
    {
        GameEventManager.instance.notebookEvents.PhotoForwardButtonPressed();
    }

    public void BackButtonPressed()
    {
        GameEventManager.instance.notebookEvents.PhotoBackButtonPressed();
    }

    public void DeletePhotoButtonPressed()
    {
        GameEventManager.instance.notebookEvents.PhotoDeletebuttonPressed();
    }
}
