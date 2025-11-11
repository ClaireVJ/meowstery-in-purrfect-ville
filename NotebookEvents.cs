using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookEvents
{
    public event Action OnNotebookOpened;
    public void OpenNotebook()
    {
        OnNotebookOpened?.Invoke();
    }

    public event Action OnNotebookClosed;
    public void CloseNotebook()
    {
        OnNotebookClosed?.Invoke();
    }

    public event Action<TabType> OnChangeTab;
    public void OpenTab(TabType tabType)
    {
        OnChangeTab?.Invoke(tabType);
    }

    public event Action OnCloseTab;
    public void CloseTab()
    {
        OnCloseTab?.Invoke();
    }

    // Photo
    public event Action OnPhotoPageOpen;
    public void OpenPhotoPage()
    {
        OnPhotoPageOpen?.Invoke();
    }

    public event Action OnPhotoForwardButtonPressed;
    public void PhotoForwardButtonPressed()
    {
        OnPhotoForwardButtonPressed?.Invoke();
    }

    public event Action OnPhotoBackButtonPressed;
    public void PhotoBackButtonPressed()
    {
        OnPhotoBackButtonPressed?.Invoke();
    }

    public event Action OnPhotoDeleteButtonPressed;
    public void PhotoDeletebuttonPressed()
    {
        OnPhotoDeleteButtonPressed?.Invoke();
    }

    public event Action OnTaskPageOpen;
    public void OpenTaskPage()
    {
        OnTaskPageOpen?.Invoke();
    }
}
