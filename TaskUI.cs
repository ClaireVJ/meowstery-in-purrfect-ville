using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskUI : MonoBehaviour
{
    [SerializeField] private Task taskInfo;

    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private TextMeshProUGUI taskInfoText;

    public void Init(Task task, string currentGoalName, string currentGoalDetails)
    {
        taskInfo = task;

        gameObject.name = taskInfo.taskInfoSO.taskID;
        ResetTaskDisplay(currentGoalName, currentGoalDetails);
    }

    private void ResetTaskDisplay(string currentGoalName, string currentGoalDetails)
    {
        displayNameText.text = currentGoalName;
        taskInfoText.text = currentGoalDetails;
    }

    public Task GetTaskInfo()
    {
        return taskInfo;
    }
}
