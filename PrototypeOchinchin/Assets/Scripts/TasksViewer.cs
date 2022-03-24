using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksViewer : MonoBehaviour
{
    private Text text;
    void Awake()
    {
        text = GetComponent<Text>();
    }

    public void SetTasksInfo(int completedTasks, int countTasks)
    {
        text.text = $"{completedTasks}/{countTasks}";
    }
}
