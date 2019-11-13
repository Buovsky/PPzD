using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] Bandit banditController;
    [SerializeField] PlayerSubsystem[] subsystems;

    bool initialised;


    void Awake() {
        Initialise();
    }

    void Initialise() {
        if (initialised)
            return;
        foreach (var subsystem in subsystems)
            subsystem.Initialise(this);
        banditController.JumpedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Jump);
        banditController.LandedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Landing);
        banditController.AttackedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Attack);
        banditController.DiedEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Death);
        banditController.RecoveredEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Recovery);
        banditController.FootstepEvent += () => NotifySubsystemsAboutNewEvent(PlayerEventType.Footstep);
        

        initialised = true;
    }

    void NotifySubsystemsAboutNewEvent(PlayerEventType eventType) {
        foreach (var playerSubsystem in subsystems)
            playerSubsystem.HandleEvent(eventType);
    }
}

public abstract class PlayerSubsystem : MonoBehaviour {
    protected PlayerController player;

    public void Initialise(PlayerController player) {
        this.player = player;
    }

    public abstract void HandleEvent(PlayerEventType eventType);
}

public enum PlayerEventType {
    Jump,
    Landing,
    Death,
    Recovery,
    Attack,
    Footstep
}