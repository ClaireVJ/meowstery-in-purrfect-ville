using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPoint : MonoBehaviour
{
    private EventTrigger eventTrigger;

    [Header("Task")]
    [SerializeField] private TaskInfoSO taskForThisPoint;
    private string taskID;
    private TaskState currentTaskState;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
        taskID = taskForThisPoint.taskID;
    }

    private void OnEnable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.interactEvents.OnInteractPressed += InteractPressed;
            GameEventManager.instance.taskEvents.OnTaskStateChange += TaskStateChanged;
        }
    }

    private void OnDisable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.interactEvents.OnInteractPressed -= InteractPressed;
            GameEventManager.instance.taskEvents.OnTaskStateChange -= TaskStateChanged;
        }
    }

    // Called when OnTaskStateChange is Invoked
    private void TaskStateChanged(Task task)
    {
        // If the task that changed IS the task assigned to this task point
        if (task.taskInfoSO.taskID.Equals(taskID))
        {
            // Update this task point current task state to the task that changed current task state
            currentTaskState = task.currentTaskState;
        }
    }

    // Called when OnInteractPressed is Invoked (When the player presses E while in interact range)
    private void InteractPressed()
    {
        // If the player isn't within interacting range, don't allow it to continue this function
        if (eventTrigger.CanAcceptTask() == false)
        {
            return;
        }

        // If we can start the task assigned to this task point AND this is the task start point
        if (currentTaskState.Equals(TaskState.CAN_START) && startPoint)
        {
            GameEventManager.instance.taskEvents.StartTask(taskID);
        }
        // If we can finish the task assigned to this task point AND this is the task finish point
        else if (currentTaskState.Equals(TaskState.CAN_FINISH) && finishPoint)
        {
            GameEventManager.instance.taskEvents.FinishTask(taskID);
        }
    }

    // Called in EventTrigger's OnTriggerEnter() to check if the player can dialogue with this NPC
    // Only relevant when NPC has both dialogue AND task
    public TaskState GetCurrentTaskState()
    {
        return currentTaskState;
    }
}
