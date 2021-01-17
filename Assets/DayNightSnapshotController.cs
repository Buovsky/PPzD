using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSnapshotController : MonoBehaviour
{
    [SerializeField, EventRef] string snapshotDay;
    [SerializeField, EventRef] string snapshotDaySunset;
    [SerializeField, EventRef] string snapshotDayNight;

    private FMOD.Studio.EventInstance snapshotInstanceDay;
    private FMOD.Studio.EventInstance snapshotInstanceSunset;
    private FMOD.Studio.EventInstance snapshotInstanceNight;

    void Awake()
    {
        DayNightController.TimeOfDayChangedEvent += OnTimeOfDayChanged;
    }

    void OnDestroy()
    {
        DayNightController.TimeOfDayChangedEvent -= OnTimeOfDayChanged;
    }

    void OnTimeOfDayChanged(TimeOfDay timeOfDay)
    {
        switch (timeOfDay)
        {
            case TimeOfDay.Day:
                OnDay();
                break;
            case TimeOfDay.Sunset:
                OnSunset();
                break;
            case TimeOfDay.Night:
                OnNight();
                break;
        }
    }

    private void OnDay()
    {
        snapshotInstanceNight.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        snapshotInstanceNight.release();
        snapshotInstanceNight.clearHandle();

        Debug.Log("Day SNAPSHOT");

        snapshotInstanceDay = RuntimeManager.CreateInstance(snapshotDay);
        snapshotInstanceDay.start();
        snapshotInstanceDay.triggerCue();
    }

    private void OnSunset()
    {
        snapshotInstanceDay.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        snapshotInstanceDay.release();
        snapshotInstanceDay.clearHandle();

        Debug.Log("Sunset SNAPSHOT");

        snapshotInstanceSunset = RuntimeManager.CreateInstance(snapshotDaySunset);
        snapshotInstanceSunset.start();
        snapshotInstanceSunset.triggerCue();
    }

    private void OnNight()
    {
        snapshotInstanceSunset.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        snapshotInstanceSunset.release();
        snapshotInstanceSunset.clearHandle();

        Debug.Log("NIGHT//SNAPSHOT");

        snapshotInstanceNight = RuntimeManager.CreateInstance(snapshotDayNight);
        snapshotInstanceNight.start();
        snapshotInstanceNight.triggerCue();
    }
}
