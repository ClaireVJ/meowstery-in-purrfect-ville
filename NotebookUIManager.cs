using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookUIManager : MonoBehaviour
{
    [SerializeField] private GameObject tabsUI;
    [SerializeField] private GameObject backgroundUI;

    [SerializeField] private GameObject keyInfoContent;
    [SerializeField] private GameObject photoContent;
    [SerializeField] private GameObject settingContent;
    [SerializeField] private GameObject tasksContent;

    private void OnEnable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.notebookEvents.OnNotebookOpened += ShowNotebook;
            GameEventManager.instance.notebookEvents.OnNotebookClosed += HideNotebook;
            GameEventManager.instance.notebookEvents.OnChangeTab += ChangeTab;
        }
    }

    private void OnDisable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.notebookEvents.OnNotebookOpened -= ShowNotebook;
            GameEventManager.instance.notebookEvents.OnNotebookClosed -= HideNotebook;
            GameEventManager.instance.notebookEvents.OnChangeTab -= ChangeTab;
        }
    }

    // Called when OnNotebookOpened is Invoked
    private void ShowNotebook()
    {
        PlayerManager.instance.SwitchCurrentPlayerState(PlayerState.UI);
        ChangeTab(TabType.KEYINFORMATION);
        tabsUI.SetActive(true);
        backgroundUI.SetActive(true);
    }

    // Called when OnNotebookClosed is Invoked
    private void HideNotebook()
    {
        PlayerManager.instance.SwitchCurrentPlayerState(PlayerManager.instance.GetPreviousPlayerState());
        tabsUI.SetActive(false);
        backgroundUI.SetActive(false);

        keyInfoContent.SetActive(false);
        photoContent.SetActive(false);
        tasksContent.SetActive(false);
        settingContent.SetActive(false);
    }

    // Called when OnChangeTab is Invoked
    private void ChangeTab(TabType tabType)
    {
        // Invoke the "OnCloseTab"? event
        GameEventManager.instance.notebookEvents.CloseTab();

        // Set everything related to tabs to false
        keyInfoContent.SetActive(false);
        photoContent.SetActive(false);
        tasksContent.SetActive(false);
        settingContent.SetActive(false);

        switch (tabType)
        {
            case TabType.KEYINFORMATION:
                keyInfoContent.SetActive(true);
                break;

            case TabType.TASKS:
                tasksContent.SetActive(true);
                GameEventManager.instance.notebookEvents.OpenTaskPage();
                break;

            case TabType.PHOTODISPLAY:
                photoContent.SetActive(true);
                GameEventManager.instance.notebookEvents.OpenPhotoPage();
                break;

            case TabType.SETTINGS:
                settingContent.SetActive(true);
                break;

            default:
                Debug.LogWarning("Outside of possible tabTypes");
                break;
        }
    }
}
