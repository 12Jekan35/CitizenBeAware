using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TasksViewer tasksViewer;
    [SerializeField] private InteractObject[] tasks;
    [SerializeField, Range(0, 1)] private float CompleteTime;
    [SerializeField] private InteractObject ExitObject;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ExitObject.IsInteract += CompleteLevel;
    }

    public void CompleteLevel()
    {
        Pause.Instance.End();
        if (endPanel != null)
        {
            endPanel.SetActive(true);
            int counter = 0;
            foreach (var task in tasks)
                if (task.Completed)
                    counter++;
            tasksViewer.SetTasksInfo(counter, tasks.Length);
        }

        if(TimeManager.Instance.timeProgress > CompleteTime)
        {
            Debug.Log("Ты лох");
        }
    }

}
