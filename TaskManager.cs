using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Dictionary<string, Task> taskDictionary { get; private set; }

    private void OnEnable()
    {
        GameEventManager.instance.taskEvents.OnTaskStart += StartTask;
        GameEventManager.instance.taskEvents.OnTaskAdvanced += AdvanceTask;
        GameEventManager.instance.taskEvents.OnTaskFinished += FinishTask;
    }

    private void OnDisable()
    {
        GameEventManager.instance.taskEvents.OnTaskStart -= StartTask;
        GameEventManager.instance.taskEvents.OnTaskAdvanced -= AdvanceTask;
        GameEventManager.instance.taskEvents.OnTaskFinished -= FinishTask;
    }

    private void Start()
    {
        taskDictionary = CreateTaskDictionary();
    }

    private void Update()
    {
        foreach (Task task in taskDictionary.Values)
        {
            if (task.currentTaskState == TaskState.REQUIREMENTS_NOT_MET && CheckIfRequirementsAreMet(task))
            {
                ChangeTaskState(task, TaskState.CAN_START);
            }
        }
    }

    private void StartTask(string taskToStartID)
    {
        Debug.Log("Start task : " + taskToStartID);

        Task task = GetTaskByID(taskToStartID);
        ChangeTaskState(task, TaskState.IN_PROGRESS);
        task.InstantiateGoalPrefab(this.transform);

        GameEventManager.instance.taskEvents.TaskUIUpdate(task);
    }

    private void AdvanceTask(string taskToAdvanceID)
    {
        Debug.Log("Advance task : " + taskToAdvanceID);

        Task task = GetTaskByID(taskToAdvanceID);
        task.IncreaseCurrentGoalIndex();
        task.InstantiateGoalPrefab(this.transform);
    }

    private void FinishTask(string taskToFinishID)
    {
        Debug.Log("Finish task : " + taskToFinishID);

        Task task = GetTaskByID(taskToFinishID);
        ChangeTaskState(task, TaskState.FINISHED);

        GameEventManager.instance.taskEvents.TaskUIUpdate(task);
    }

    private Task GetTaskByID(string taskID)
    {
        if (taskDictionary.TryGetValue(taskID, out Task task))
        {
            return task;
        }
        else
        {
            Debug.LogWarning("WARNING : Couldn't find task by ID - Does the task exist in the task dictionary?");
            return null;
        }
    }

    private void ChangeTaskState(Task task, TaskState newTaskState)
    {
        task.currentTaskState = newTaskState;
        GameEventManager.instance.taskEvents.TaskStateChange(task);
    }

    private bool CheckIfRequirementsAreMet(Task task)
    {
        bool metRequirements = true;

        if (task.taskInfoSO.requirements.Length > 0)
        {
            metRequirements = false;
        }

        return metRequirements;
    }

    private Dictionary<string, Task> CreateTaskDictionary()
    {
        Dictionary<string, Task> newTaskDictionary = new Dictionary<string, Task>();

        // Find all the TaskInfoSO in the Resources folder
        TaskInfoSO[] taskList = Resources.FindObjectsOfTypeAll<TaskInfoSO>();

        // For each TaskInfoSO found in the resources folder...
        foreach (TaskInfoSO task in taskList)
        {
            // Check if the dictionary contains the task found in the resources folder (it shouldn't)
            if (newTaskDictionary.ContainsKey(task.taskID))
            {
                Debug.LogWarning("WARNING : Duplicate key found in taskDictionary");
            }

            // Add the task to the dictionary
            newTaskDictionary.Add(task.taskID, new Task(task));
        }

        return newTaskDictionary;
    }
}
