using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InteractEvents
{
    // When the player presses the interact key while not in dialogue
    public event Action OnInteractPressed;
    public void OnInteract()
    {
        OnInteractPressed?.Invoke();
    }

    // When the player presses the interact key while in dialogue
    public event Action OnDialogueContinue;
    public void ContinueDialogue()
    {
        OnDialogueContinue?.Invoke();
    }

    // When you enter a dialogue with an NPC (share their knot name with relevant scripts)
    public event Action<string, Sprite> OnDialogueEntered;
    public void EnterDialogue(string knotName, Sprite sprite)
    {
        OnDialogueEntered?.Invoke(knotName, sprite);
    }

    // Let other scripts know that dialogue has started (UI)
    public event Action OnDialogueHasStarted;
    public void DialogueHasStarted()
    {
        OnDialogueHasStarted?.Invoke();
    }

    // Let other scripts know that dialogue has ended (UI)
    public event Action OnDialogueHasEnded;
    public void DialogueHasEnded()
    {
        OnDialogueHasEnded?.Invoke();
    }

    // For UI, to know what dialogue lines and choices to display
    public event Action<string, List<Choice>> OnDialogueDisplay;
    public void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        OnDialogueDisplay?.Invoke(dialogueLine, dialogueChoices);
    }

    // Called when there are choices, 
    public event Action<int> OnUpdateChoiceIndex;
    public void UpdateChoiceIndex(int choiceIndex)
    {
        OnUpdateChoiceIndex?.Invoke(choiceIndex);
    }

    public event Action<string> OnSpeakerNameChange;
    public void UpdateSpeakerName(string updatedName)
    {
        OnSpeakerNameChange?.Invoke(updatedName);
    }

    public event Action<string> OnDialogueSideChange;
    public void UpdateDialogueSide(string updatedSide)
    {
        OnDialogueSideChange?.Invoke(updatedSide);
    }

    public event Action<string> OnPortraitChange;
    public void UpdatePortrait(string updatedPortraitName)
    {
        OnPortraitChange?.Invoke(updatedPortraitName);
    }

    public event Action<List<Sprite>> OnPortraitSpritesNeeded;
    public void GetPortraitSprites(List<Sprite> neededSprites)
    {
        OnPortraitSpritesNeeded?.Invoke(neededSprites);
    }
}
