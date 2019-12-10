using System;
using UnityEngine;
using UnityEngine.Audio;

public class ForestAreaSoundController : MonoBehaviour {
    [SerializeField] AmbienceSnapshotsDatabase snapshots;

    const float SnapshotTransitionTime = .5f;

    TimeOfDay currentTimeOfDay;
    bool isInForest;


    void Start() {
        DayNightController.TimeOfDayChangedEvent += HandleTimeofDayChanged;
    }

    void HandleTimeofDayChanged(TimeOfDay newTimeOfDay) {
        currentTimeOfDay = newTimeOfDay;
        UpdateSnapshotAccordingToNewConditions();
    }

    void OnTriggerEnter2D(Collider2D other) {
        isInForest = true;
        UpdateSnapshotAccordingToNewConditions();
    }

    void OnTriggerExit2D(Collider2D other) {
        isInForest = false;
        UpdateSnapshotAccordingToNewConditions();
    }

    void UpdateSnapshotAccordingToNewConditions() {
        var newSnapshot = snapshots.GetSnapshotMatchingConditions(currentTimeOfDay, isInForest);
        newSnapshot.TransitionTo(SnapshotTransitionTime);
    }

    void TransitionToSnapshot(AudioMixerSnapshot newSnapshot) {
        newSnapshot.TransitionTo(SnapshotTransitionTime);
    }

    [Serializable]
    class AmbienceSnapshotsDatabase {
        [SerializeField] AudioMixerSnapshot plainsSnapshotDay;
        [SerializeField] AudioMixerSnapshot plainsSnapshotSunset;
        [SerializeField] AudioMixerSnapshot plainsSnapshotNight;
        [SerializeField] AudioMixerSnapshot forestSnapshotDay;
        [SerializeField] AudioMixerSnapshot forestSnapshotSunset;
        [SerializeField] AudioMixerSnapshot forestSnapshotNight;


        public AudioMixerSnapshot GetSnapshotMatchingConditions(TimeOfDay timeOfDay, bool isInForest) {
            switch (timeOfDay) {
                case TimeOfDay.Day:
                    return isInForest ? forestSnapshotDay : plainsSnapshotDay;
                case TimeOfDay.Sunset:
                    return isInForest ? forestSnapshotSunset : plainsSnapshotSunset;
                case TimeOfDay.Night:
                    return isInForest ? forestSnapshotNight : plainsSnapshotNight;
                default:
                    throw new ArgumentOutOfRangeException(nameof(timeOfDay), timeOfDay, null);
            }
        }
    }
}

