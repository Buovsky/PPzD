using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceEventController : MonoBehaviour
{
    [SerializeField, EventRef] string eventAudioBirds;
    [SerializeField, EventRef] string eventAudioWind;
    [SerializeField, EventRef] string eventAudioCrikets;

    private FMOD.Studio.EventInstance musicInstanceInForest;
    private FMOD.Studio.EventInstance musicInstanceOutForest;
    private FMOD.Studio.EventInstance musicInstanceOutForestSecond;

    private bool wasOutForest = false;

    private void Start()
    {
        musicInstanceInForest = RuntimeManager.CreateInstance(eventAudioBirds);
        musicInstanceOutForest = RuntimeManager.CreateInstance(eventAudioWind);
        musicInstanceOutForestSecond = RuntimeManager.CreateInstance(eventAudioCrikets);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            musicInstanceOutForest.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstanceOutForestSecond.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            
            musicInstanceInForest.start();
            musicInstanceInForest.triggerCue();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wasOutForest = true;

            musicInstanceInForest.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            musicInstanceOutForest.start();
            musicInstanceOutForest.triggerCue();

            musicInstanceOutForestSecond.start();
            musicInstanceOutForestSecond.triggerCue();
        }
    }
}
