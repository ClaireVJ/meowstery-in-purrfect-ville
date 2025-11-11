using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJson;
    private Story story;

    private bool isDialoguePlaying = false;
    private int currentChoiceIndex = -1;

    private const string SPEAKER_TAG = "Speaker";
    private const string SIDE_TAG = "Side";
    private const string PORTRAIT_TAG = "Portrait";

    [SerializeField] private List<Sprite> playerSprites;

    private void Awake()
    {
        story = new Story(inkJson.text);
    }

    private void OnEnable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.interactEvents.OnDialogueEntered += OnDialogueEnter;
            GameEventManager.instance.interactEvents.OnDialogueContinue += OnDialogueContinue;
            GameEventManager.instance.interactEvents.OnUpdateChoiceIndex += UpdateChoiceIndex;
        }
    }


    private void OnDisable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.interactEvents.OnDialogueEntered -= OnDialogueEnter;
            GameEventManager.instance.interactEvents.OnDialogueContinue -= OnDialogueContinue;
            GameEventManager.instance.interactEvents.OnUpdateChoiceIndex -= UpdateChoiceIndex;
        }
    }

    // How to respond when OnDialogueContinue is Invoked/Sent out/Done
    private void OnDialogueContinue()
    {
        // Check if dialogue isn't playing, if it isn't, don't allow this function to continue
        if (!isDialoguePlaying)
        {
            Debug.LogWarning("Tried to continue dialogue but dialogue isnt playing");
            return;
        }

        ContinueDialogue();
    }

    // How to respond when UpdateChoiceIndex is Invoked/Sent out/Done
    private void UpdateChoiceIndex(int choiceIndex)
    {
        // Set current choice index to choice index sent out by this event
        this.currentChoiceIndex = choiceIndex;
    }

   // How to respond when OnDialogueEntered is Invoked/Sent out/Done
    private void OnDialogueEnter(string dialogueKnotName, Sprite currentNPCSprite)
    {
        //Check if dialogue is already playing, if it is, do not continue this method (return)
        if (isDialoguePlaying)
        {
            Debug.Log("You dummy, dialogue is ALREADY playing");
            return;
        }

        isDialoguePlaying = true;

        List<Sprite> neededSprites = new List<Sprite>();
        neededSprites.Add(currentNPCSprite);
        
        foreach (Sprite sprite in playerSprites)
        {
            neededSprites.Add(sprite);
        }

        GameEventManager.instance.interactEvents.GetPortraitSprites(neededSprites);
        neededSprites.Clear();

        // Switch the player into INTERACTING mode (stop the playe from moving, jumping, etc)
        PlayerManager.instance.SwitchCurrentPlayerState(PlayerState.INTERACTING);

        // Let others gameObjects/scripts know that dialogue has started
        GameEventManager.instance.interactEvents.DialogueHasStarted();

        // Check if the knot name does not equal null/nothing, if it doesn't, jump to the knot.
        if (!dialogueKnotName.Equals(""))
        {
            story.ChoosePathString(dialogueKnotName);
        }
        // If it is null, display a warning
        else
        {
            Debug.LogWarning("dialogueKnotName was empty/null.");
        }

        // Start the dialogue
        ContinueDialogue();
    }

    private void ContinueDialogue()
    {
        // If there are choices the player can make AND currentChoiceIndex isnt null(-1)
        if (story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            // Choose a choice based on the currentChoiceIndex
            story.ChooseChoiceIndex(currentChoiceIndex);

            //reset choice index for next time
            currentChoiceIndex = -1;
        }

        // Check if there is more story/lines of dialogue (that is not a choice point or the end)
        if (story.canContinue)
        {
            // Set a local variable to 1 line of dialogue from the story using Ink's function (Continue())
            string dialogueLine = story.Continue();

            // Handle tags
            HandleTags(story.currentTags);

            // While loops are ones that continue iterating until the provided statement is false.
            // While there's a blank line BUT there are other lines of dialogue after...
            while (IsLineBlank(dialogueLine) && story.canContinue)
            {
                // Set dialogue line to the next line
                dialogueLine = story.Continue();
            }

            // If there is a blank line BUT there are no more lines of dialogue after
            if (IsLineBlank(dialogueLine) && !story.canContinue)
            {
                // Call the ExitDialogue function
                ExitDialogue();
            }
            // Any other scenerio
            else
            {
                GameEventManager.instance.interactEvents.DisplayDialogue(dialogueLine, story.currentChoices);
            }
        }
        else if (story.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        isDialoguePlaying = false;

        // Switch the player into NORMAL mode (allow the player to move, jump, etc)
        PlayerManager.instance.SwitchCurrentPlayerState(PlayerState.NORMAL);

        // Let others gameObjects/scripts know that dialogue has ended
        GameEventManager.instance.interactEvents.DialogueHasEnded();

        // set the story to a blank state
        story.ResetState();
    }

    private void HandleTags(List<string> currentTags)
    {
        // Go through each tag pair
        foreach (string tag in currentTags)
        {
            // Parse the tag
            string[] splitTag = tag.Split(':');

            if (splitTag.Length != 2)
            {
                Debug.LogWarning("Couldn't parse tag: " + tag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Handle the tag 
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    GameEventManager.instance.interactEvents.UpdateSpeakerName(tagValue);
                    break;
                case SIDE_TAG:
                    GameEventManager.instance.interactEvents.UpdateDialogueSide(tagValue);
                    break;
                case PORTRAIT_TAG:
                    if (tagValue != "Player")
                    {
                        GameEventManager.instance.interactEvents.UpdatePortrait(tagValue);
                    }
                    else if (tagValue == "Player")
                    {
                        GameEventManager.instance.interactEvents.UpdatePortrait(tagValue);
                    }
                        break;
                default:
                    Debug.LogWarning("Unknown tag type");
                    break;
            }
        }
    }

    private bool IsLineBlank(string dialogueLine)
    {
        //Trim() is used to remove leading/trailing whitespace characters from a string.
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }
}
