using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    // Static (non-changing) information
    public TaskInfoSO taskInfoSO;

    // Changing information
    public TaskState currentTaskState;
    public int currentGoalIndex;

    public TaskGoal currentTaskGoal;

    public Task(TaskInfoSO taskInfoSO)
    {
        this.taskInfoSO = taskInfoSO;
        currentTaskState = TaskState.REQUIREMENTS_NOT_MET;
        currentGoalIndex = 0;
    }

    // Increase currentGoalIndex by 1
    public void IncreaseCurrentGoalIndex()
    {
        currentGoalIndex++;
    }

    public void InstantiateGoalPrefab(Transform parentToInstantiateIn)
    {
        if (currentGoalIndex >= taskInfoSO.goals.Length)
        {
            GameEventManager.instance.taskEvents.FinishTask(taskInfoSO.taskID);
            return;
        }

        GameObject goalPrefab = GameObject.Instantiate(taskInfoSO.goals[currentGoalIndex], parentToInstantiateIn);
        currentTaskGoal = goalPrefab.GetComponent<TaskGoal>();
        currentTaskGoal.Init(taskInfoSO.taskID);
    }
}
