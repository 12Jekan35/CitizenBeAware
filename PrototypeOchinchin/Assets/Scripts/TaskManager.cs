using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }
    private GameObject endPanel;

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

    private void CompleteLevel()
    {
        Pause.Instance.End();
        if (endPanel != null)
        {
            endPanel.SetActive(true);
        }
    }

}
