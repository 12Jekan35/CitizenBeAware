using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private Text CurrentTime;
    private TimeManager timeManager;
    void Start()
    {
        timeManager = TimeManager.Instance;
        Debug.Log($"{timeManager.timeInMinutes}");
        CurrentTime = GetComponent<Text>();
        timeManager.TimeEvent += TimeHandler;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void TimeHandler(int timeInMinutes)
    {
        
        int hours = timeInMinutes / 60;
        int minutes = timeInMinutes % 60;
        CurrentTime.text = $"{hours}:{minutes}";
        
        Debug.Log($"{hours}:{minutes}");
    }
}
