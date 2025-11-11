using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskEvents
{
    public event Action<string> OnTaskStart;
    public void StartTask(string id)
    {
        OnTaskStart?.Invoke(id);
    }

    public event Action<string> OnTaskFinished;
    public void FinishTask(string id)
    {
        OnTaskFinished?.Invoke(id);
    }

    public event Action<string> OnTaskAdvanced;
    public void AdvanceTask(string id)
    {
        OnTaskAdvanced?.Invoke(id);
    }

    public event Action<Task> OnTaskStateChange;
    public void TaskStateChange(Task task)
    {
        OnTaskStateChange?.Invoke(task);
    }

    public event Action<Task> OnTaskUIUpdate;
    public void TaskUIUpdate(Task task)
    {
        OnTaskUIUpdate?.Invoke(task);
    }
}
