using UnityEngine;
using System.Collections.Generic;

public class LightBlinkManager : MonoBehaviour
{
    [Header("Blink Settings")]
    public float minBlinkTime = 0.1f;
    public float maxBlinkTime = 1.5f;
    public string ignoreTag = "NoBlink";

    private Dictionary<Light, float> nextToggleTime = new();
    private Dictionary<Light, bool> lightStates = new();

    void Start()
    {
        var lights = FindObjectsByType<Light>(FindObjectsSortMode.None);

        foreach (Light l in lights)
        {
            if (!l || l.CompareTag(ignoreTag))
                continue;

            lightStates[l] = l.enabled;
            ScheduleNext(l);
        }
    }

    void Update()
    {
        float time = Time.time;

        // copy keys to avoid modifying collection while iterating
        var lights = new List<Light>(nextToggleTime.Keys);

        foreach (Light l in lights)
        {
            if (!l)
            {
                nextToggleTime.Remove(l);
                continue;
            }

            if (time >= nextToggleTime[l])
            {
                l.enabled = !l.enabled;
                ScheduleNext(l);
            }
        }
    }

    void ScheduleNext(Light l)
    {
        nextToggleTime[l] = Time.time + Random.Range(minBlinkTime, maxBlinkTime);
    }
}
