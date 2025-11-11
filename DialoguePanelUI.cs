using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialoguePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Image portrait;
    private List<Sprite> portraitSprites;

    private Animator sideAnimator;

    private void Awake()
    {
        sideAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.interactEvents.OnDialogueHasStarted += ShowDialogueUI;
            GameEventManager.instance.interactEvents.OnDialogueHasEnded += HideDialogueUI;
            GameEventManager.instance.interactEvents.OnDialogueDisplay += UpdateDialogueUI;

            GameEventManager.instance.interactEvents.OnSpeakerNameChange += UpdateSpeakerUI;
            GameEventManager.instance.interactEvents.OnDialogueSideChange += UpdateSideUI;
            GameEventManager.instance.interactEvents.OnPortraitChange += UpdatePortraitUI;
            GameEventManager.instance.interactEvents.OnPortraitSpritesNeeded += GetPortraitSprites;
        }
    }

    private void OnDisable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.interactEvents.OnDialogueHasStarted -= ShowDialogueUI;
            GameEventManager.instance.interactEvents.OnDialogueHasEnded -= HideDialogueUI;
            GameEventManager.instance.interactEvents.OnDialogueDisplay -= UpdateDialogueUI;

            GameEventManager.instance.interactEvents.OnSpeakerNameChange -= UpdateSpeakerUI;
            GameEventManager.instance.interactEvents.OnDialogueSideChange -= UpdateSideUI;
            GameEventManager.instance.interactEvents.OnPortraitChange -= UpdatePortraitUI;
            GameEventManager.instance.interactEvents.OnPortraitSpritesNeeded -= GetPortraitSprites;
        }
    }

    // Called when OnDialogueHasStarted is Invoked
    private void ShowDialogueUI()
    {
        contentParent.SetActive(true);
    }

    // Called when OnDialogueDisplay is Invoked
    private void UpdateDialogueUI(string dialogueLines, List<Choice> dialogueChoices)
    {
        // Update the UI to show the current dialogue line
        dialogueText.text = dialogueLines;

        // If the amount of choices available is greater than the amount of choice buttons available, do a warning
        if (dialogueChoices.Count > choiceButtons.Length)
        {
            Debug.LogWarning("More dialogue choices (" + dialogueChoices.Count + ") than dialogue choice buttons(" + choiceButtons.Length + ")!");
        }

        // Hide all dialogue choices
        foreach (DialogueChoiceButton dialogueChoiceButton in choiceButtons)
        {
            dialogueChoiceButton.gameObject.SetActive(false);
        }

        // Enable and set info for buttons depending on ink choice information
        // For loops are used to iterate a fixed number of times. 

        // .Count provides the current number of elements added to the list (which is choice Buttons in this case)
        // = the current amount of choices then substracts 1
        int choiceButtonIndex = dialogueChoices.Count - 1;

        // Until the currentChoiceIndex is equal to the amount of dialogue choices, currentChoiceIndex goes up every time this loop is run
        for (int currentInkChoiceIndex = 0; currentInkChoiceIndex  < dialogueChoices.Count; currentInkChoiceIndex++)
        {
            // Get the correct dialogue choice (to get access to the text in the choice)
            Choice dialogueChoice = dialogueChoices[currentInkChoiceIndex];

            // Get the correct dialogue Button from the list of choice Buttons
            DialogueChoiceButton dialogueChoiceButton = choiceButtons[choiceButtonIndex];

            // Show the button, set its text and choiceIndex
            dialogueChoiceButton.gameObject.SetActive(true);
            dialogueChoiceButton.SetChoiceText(dialogueChoice.text);
            dialogueChoiceButton.SetChoiceIndex(currentInkChoiceIndex);

            // Substracts 1 from the choiceButtonIndex
            choiceButtonIndex--;
        }
    }

    private void UpdateSpeakerUI(string updatedName)
    {
        speakerName.text = updatedName;
    }

    private void UpdateSideUI(string updatedSide)
    {
        sideAnimator.Play(updatedSide);
    }

    private void UpdatePortraitUI(string updatedPortrait)
    {
        foreach (Sprite sprite in portraitSprites)
        {
            if (sprite.name == updatedPortrait)
            {
                portrait.sprite = sprite;
                return;
            }
        }
    }

    private void GetPortraitSprites(List<Sprite> neededSprites)
    {
        portraitSprites = new List<Sprite>();

        foreach (Sprite sprite in neededSprites)
        {
            portraitSprites.Add(sprite);
        }
    }

    // Called when OnDialogueHasEnded is Invoked
    private void HideDialogueUI()
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    // Make the text blank
    private void ResetPanel()
    {
        dialogueText.text = "";
        speakerName.text = "";
        portraitSprites.Clear();
    }
}
