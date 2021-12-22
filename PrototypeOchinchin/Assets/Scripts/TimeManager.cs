using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TimeManager : MonoBehaviour
{
    public delegate void TimeChanged(int time);
    public event TimeChanged TimeEvent;
    public static TimeManager Instance {get; private set;}
    [Header("Light")]
    [SerializeField] Light directionalLight;
    [SerializeField] private Gradient directionalLightGradient;
    [SerializeField] private Gradient ambientLightGradient;
    private Vector3 defaultAngles;

    [Header("Time")]
    [SerializeField, Range(1, 3600)] private float timeDayInSeconds = 60;
    [SerializeField, Range(0, 1)] public float timeProgress;
    [HideInInspector] public int timeInMinutes = 0;
    private const int timeDayInMinutes = 1440;
    private bool withLight = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        if (directionalLight != null)
        {
            defaultAngles = directionalLight.transform.localEulerAngles;
            withLight = true;
        }
        else
        {
            withLight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            timeProgress += Time.deltaTime / timeDayInSeconds;
        }

        if (timeProgress >= 1f)
        {
            timeProgress = 0f;
            TaskManager.Instance.CompleteLevel();
        }
        timeInMinutes = Mathf.RoundToInt(timeDayInMinutes * timeProgress);

        if (withLight)
        {
            RenderSettings.ambientLight = ambientLightGradient.Evaluate(timeProgress);
            directionalLight.color = directionalLightGradient.Evaluate(timeProgress);
            directionalLight.transform.localEulerAngles = new Vector3(360 * timeProgress - 90, defaultAngles.x, defaultAngles.z);
        }
        TimeEvent?.Invoke(timeInMinutes);

       
    }



}
