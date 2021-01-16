using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudioController : PlayerSubsystem {
    

    [SerializeField, EventRef] string eventJumpAudio;
    [SerializeField, EventRef] string eventLandingAudio;
    [SerializeField, EventRef] string eventDeathAudio;
    [SerializeField, EventRef] string eventAttackAudio;
    [SerializeField, EventRef] string eventFootsteepAudio;
    [SerializeField, EventRef] string eventBlockAudio;
    [SerializeField, EventRef] string eventDamageAudio;

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
}
