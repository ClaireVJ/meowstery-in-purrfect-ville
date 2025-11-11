using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    private bool canDialogue;
    private bool canAcceptTask;

    [SerializeField] private List<TaskState> cantDialogueStates;

    private void Awake()
    {
        cantDialogueStates = new List<TaskState>();
        cantDialogueStates.Add(TaskState.CAN_START);
        cantDialogueStates.Add(TaskState.IN_PROGRESS);
        cantDialogueStates.Add(TaskState.CAN_FINISH);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If it's the player (by checking if the playerInteract script is on the gameObject that entered the trigger)
        if (other.gameObject.TryGetComponent(out PlayerInteract playerInteract))
        {
            if (TryGetComponent(out TaskPoint taskPoint))
            {
                while (cantDialogueStates.Contains(taskPoint.GetCurrentTaskState()))
                {
                    canDialogue = false;
                    canAcceptTask = true;
                    return;
                }

                canDialogue = true;
                canAcceptTask = false;
            }
            else
            {
                canDialogue = true;
                canAcceptTask = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If it's the player (by checking if the playerInteract script is on the gameObject that entered the trigger)
        if (other.gameObject.TryGetComponent(out PlayerInteract playerInteract))
        {
            canDialogue = false;
            canAcceptTask = false;
        }
    }

    public bool CanAcceptTask()
    {
        return canAcceptTask;
    }

    public bool CanDialogue()
    {
        return canDialogue;
    }
}
