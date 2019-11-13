using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudioController : PlayerSubsystem {
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip landingClip;
    [SerializeField] AudioClip[] footsteps;
    [SerializeField] AudioSource feetSource;

    int currentFootstepIndex = -1;


    AudioClip GetRandomFootstep() {
        var index = 0;
        while(currentFootstepIndex == index)
            index = Random.Range(0, footsteps.Length);
        currentFootstepIndex = index;
        return footsteps[index];
    }

    public override void HandleEvent(PlayerEventType eventType) {
        switch (eventType) {
            case PlayerEventType.Jump:
//                PlayClip(feetSource, jumpClip); //i.e. panting or any other extra sound to enrich jump sound
                PlayOneShot(feetSource, GetRandomFootstep());
                break;
            case PlayerEventType.Landing:
//                PlayClip(feetSource, landingClip);
                PlayOneShot(feetSource, GetRandomFootstep());
                break;
            case PlayerEventType.Death:
                break;
            case PlayerEventType.Recovery:
                break;
            case PlayerEventType.Attack:
                break;
            case PlayerEventType.Footstep:
                PlayOneShot(feetSource, GetRandomFootstep());
                break;
        }
    }

    void PlayClip(AudioSource source, AudioClip clip) {
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    void PlayOneShot(AudioSource source, AudioClip clip, float volume = 1) {
        source.PlayOneShot(clip, volume);
    }
}
