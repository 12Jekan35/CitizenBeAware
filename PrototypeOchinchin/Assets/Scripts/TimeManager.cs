using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TimeManager : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] Light directionalLight;
    [SerializeField] private Gradient directionalLightGradient;
    [SerializeField] private Gradient ambientLightGradient;

    [Header("Time")]
    [SerializeField, Range(1, 3600)] private float timeDayInSeconds = 60;
    [Range(0, 1)] private float timeProgress;
    public float TimeProgress 
    {
        get => timeProgress;
        set
        {
            timeProgress = value;
            timeInMinutes = Mathf.Round(timeDayInMinutes * timeProgress);
            RenderSettings.ambientLight = ambientLightGradient.Evaluate(timeProgress);
            if (directionalLight != null)
            {
                directionalLight.color = directionalLightGradient.Evaluate(timeProgress);
                directionalLight.transform.localEulerAngles = new Vector3(360 * timeProgress - 90, defaultAngles.x, defaultAngles.z);
            }
        }
    }
    [HideInInspector] public float timeInMinutes = 0;
    private const float timeDayInMinutes = 1440;
    private Vector3 defaultAngles;
    void Start()
    {
        defaultAngles = directionalLight.transform.localEulerAngles;
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
        }
    }
}