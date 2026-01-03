using System.Collections.Generic;
using UnityEngine;

public class LampBlink : MonoBehaviour
{
    public float minBlinkTime = 0.1f;
    public float maxBlinkTime = 1.5f;
    public List<Light> allLights;

    private float nextToggleTime;
    private bool isOn = true;

    void Start()
    {
        ScheduleNext();
    }

    void Update()
    {
        if (Time.time >= nextToggleTime)
        {
            isOn = !isOn;
            foreach (var l in allLights)
                if (l) l.enabled = isOn;

            ScheduleNext();
        }
    }

    void ScheduleNext()
    {
        nextToggleTime = Time.time + Random.Range(minBlinkTime, maxBlinkTime);
    }
}
