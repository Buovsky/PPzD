using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAudioController : MonoBehaviour{

    [SerializeField] Bandit banditController;

    //[SerializeField, EventRef] string eventJumpAudio;
    //[SerializeField, EventRef] string eventLandingAudio;
    [SerializeField, EventRef] string eventDeathAudio;
    [SerializeField, EventRef] string eventAttackAudio;
    [SerializeField, EventRef] string eventFootsteepAudio;
    //[SerializeField, EventRef] string eventBlockAudio;
    [SerializeField, EventRef] string eventDamageAudio;

    void Awake()
    {
        //banditController.JumpedEvent += 
        //banditController.LandedEvent += 
        banditController.AttackedEvent += OnAttack;
        banditController.DiedEvent += OnDeath;
        banditController.FootstepEvent += OnFootstep;
        //banditController.BlockEvent += 
        banditController.DamageEvent += OnDamage;
    }

    public void OnJump()
    {
        //RuntimeManager.PlayOneShot(eventJumpAudio);
    }

    public void OnLanding()
    {
        //RuntimeManager.PlayOneShot(eventLandingAudio);
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
       // RuntimeManager.PlayOneShot(eventBlockAudio);
    }
    public void OnDamage()
    {
        RuntimeManager.PlayOneShot(eventDamageAudio);
    }
}
