using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskPageUI : MonoBehaviour
{
    [SerializeField] private List<string> taskIDs;
    [SerializeField] private List<GameObject> taskUI;
    private TaskManager taskManager;

    [SerializeField] private GameObject taskUIPrefab;
    [SerializeField] private Transform taskContentParent;

    private void Awake()
    {
        taskManager = GameObject.FindFirstObjectByType<TaskManager>();
        taskUI = new List<GameObject>();
        taskIDs = new List<string>();
    }

    private void OnEnable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.taskEvents.OnTaskStateChange += UpdateTaskData;
            GameEventManager.instance.taskEvents.OnTaskUIUpdate += UpdateTaskUI;
        }
    }

    private void OnDisable()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance.taskEvents.OnTaskStateChange -= UpdateTaskData;
            GameEventManager.instance.taskEvents.OnTaskUIUpdate -= UpdateTaskUI;
        }
    }

    private void UpdateTaskUI(Task task)
    {
        if (task.currentTaskState == TaskState.IN_PROGRESS || task.currentTaskState == TaskState.CAN_FINISH)
        {
            CreateNewTaskUI(task);
        }
        else if (task.currentTaskState == TaskState.FINISHED)
        {
            int taskIndex = taskIDs.IndexOf(task.taskInfoSO.taskID);
            taskUI.RemoveAt(taskIndex);
            taskIDs.Remove(task.taskInfoSO.taskID);
        }
    }

    private void UpdateTaskData(Task task)
    {
        foreach (Task taskInfo in taskManager.taskDictionary.Values)
        {
            if (taskInfo == task)
            {
                int taskIndex = taskIDs.IndexOf(task.taskInfoSO.taskID);
            }
        }
    }

    private void CreateNewTaskUI(Task task)
    {
        // For each id in taskIDs
        foreach (string id in taskIDs)
        {
            // Check if we already have a id (and therefore a taskUI) in the list
            if (task.taskInfoSO.taskID == id)
            {
                return;
            }
        }

        taskIDs.Add(task.taskInfoSO.taskID);

        GameObject createdTaskUI = Instantiate(taskUIPrefab, taskContentParent);
        TaskUI taskUIScript = createdTaskUI.GetComponent<TaskUI>();

        taskUIScript.Init(task, task.currentTaskGoal.goalName, task.currentTaskGoal.goalDescription);

        taskUI.Add(createdTaskUI);
    }
}
