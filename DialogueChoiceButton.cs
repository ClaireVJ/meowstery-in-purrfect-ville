using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button choiceButton;
    [SerializeField] private TextMeshProUGUI choiceText;

    private int choiceIndex = -1;

    // Called to set the choice's text in DialoguePanelUI
    public void SetChoiceText(string choiceTextString)
    {
        choiceText.text = choiceTextString;
    }

    // Called to set the choice's index in DialoguePanelUI
    public void SetChoiceIndex(int choiceIndex)
    {
        this.choiceIndex = choiceIndex;
    }

    // For button
    public void SelectButton()
    {
        GameEventManager.instance.interactEvents.UpdateChoiceIndex(choiceIndex);
        GameEventManager.instance.interactEvents.ContinueDialogue();
    }
}
