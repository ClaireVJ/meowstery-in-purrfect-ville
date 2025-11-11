using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePoint : MonoBehaviour
{
    [SerializeField] private string dialogueKnotName;
    private NPC npcInfo;
    [SerializeField] private EventTrigger eventTrigger;

    private void OnEnable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.interactEvents.OnInteractPressed += OnInteractPress;
        }
    }

    private void OnDisable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.interactEvents.OnInteractPressed -= OnInteractPress;
        }
    }

    private void Start()
    {
        npcInfo = GetComponentInParent<NPC>();
        eventTrigger = GetComponent<EventTrigger>();

    }

    // Called when OnInteractPressed is Invoked
    private void OnInteractPress()
    {
        // If the player isn't within interacting range, don't allow it to continue this function
        if (eventTrigger.CanDialogue() == false)
        {
            return;
        }

        // If the gameObject has a knotName defined, try to start a dialogue with it
        if (!dialogueKnotName.Equals(""))
        {
            Debug.Log("You are close enough to an NPC to interact");
            GameEventManager.instance.interactEvents.EnterDialogue(dialogueKnotName, npcInfo.GetNPCInfoSO().npcSprite);
        }
    }
}
