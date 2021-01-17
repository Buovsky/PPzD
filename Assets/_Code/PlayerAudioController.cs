using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudioController : PlayerSubsystem {

    [SerializeField] Bandit banditController;

    [SerializeField, EventRef] string eventJumpAudio;
    [SerializeField, EventRef] string eventLandingAudio;
    [SerializeField, EventRef] string eventDeathAudio;
    [SerializeField, EventRef] string eventAttackAudio;
    [SerializeField, EventRef] string eventFootsteepAudio;
    [SerializeField, EventRef] string eventBlockAudio;
    [SerializeField, EventRef] string eventDamageAudio;
    [SerializeField, EventRef] string eventLowLifeAudio;

    [SerializeField, EventRef] string snapshotLowLifeAudio;

    private FMOD.Studio.EventInstance eventInstanceLowLife;
    private FMOD.Studio.EventInstance snapshotInstanceLowLife;

    public override void HandleEvent(PlayerEventType eventType) {
        switch (eventType) {
            case PlayerEventType.Jump:
                OnJump();
                break;
            case PlayerEventType.Landing:
                OnLanding();
                break;
            case PlayerEventType.Death:
                OnDeath();
                break;
            case PlayerEventType.Attack:
                OnAttack();
                break;
            case PlayerEventType.Footstep:
                OnFootstep();
                break;
            case PlayerEventType.Block:
                OnBlock();
                break;
            case PlayerEventType.Damage:
                OnDamage();
                break;
            case PlayerEventType.LowLife:
                OnLowLife();
                break;
        }
    }

    public void OnJump()
    {
        RuntimeManager.PlayOneShot(eventJumpAudio);
    }

    public void OnLanding()
    {
        RuntimeManager.PlayOneShot(eventLandingAudio);
    }

    public void OnAttack()
    {
        RuntimeManager.PlayOneShot(eventAttackAudio);
    }

    public void OnDeath()
    {
        RuntimeManager.PlayOneShot(eventDeathAudio);
        eventInstanceLowLife.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstanceLowLife.release();
        eventInstanceLowLife.clearHandle();
    }
    public void OnFootstep()
    {
        RuntimeManager.PlayOneShot(eventFootsteepAudio);
    }

    public void OnBlock()
    {
        RuntimeManager.PlayOneShot(eventBlockAudio);
    }
    public void OnDamage()
    {  
        RuntimeManager.PlayOneShot(eventDamageAudio);
    }
    public void OnLowLife()
    {

        eventInstanceLowLife.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstanceLowLife = RuntimeManager.CreateInstance(eventLowLifeAudio);

        eventInstanceLowLife.setParameterByName("Health", banditController.currentHealth);
        eventInstanceLowLife.start();

        snapshotInstanceLowLife.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        snapshotInstanceLowLife = RuntimeManager.CreateInstance(snapshotLowLifeAudio);

        snapshotInstanceLowLife.setParameterByName("Health", banditController.currentHealth);
        snapshotInstanceLowLife.start();
    }

    private void Start()
    {
        snapshotInstanceLowLife = RuntimeManager.CreateInstance(snapshotLowLifeAudio);

        snapshotInstanceLowLife.setParameterByName("Health", banditController.currentHealth);
        snapshotInstanceLowLife.start();
    }
}
