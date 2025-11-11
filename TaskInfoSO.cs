using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "New Task")]
public class TaskInfoSO : ScriptableObject
{
    public string taskName;
    public string taskID;

    public TaskInfoSO[] requirements;
    public GameObject[] goals;
}
