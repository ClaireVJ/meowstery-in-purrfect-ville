using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskGoal : MonoBehaviour
{
    public string goalName;

    [TextArea(2,5)]
    public string goalDescription;

    public bool isFinished = false;
    public string taskIDForThisGoal;

    public void Init(string taskID)
    {
        taskIDForThisGoal = taskID;
    }

    protected void CompleteGoal()
    {
        if (isFinished)
        {
            GameEventManager.instance.taskEvents.AdvanceTask(taskIDForThisGoal);
            Destroy(this.gameObject);
        }
    }
}
