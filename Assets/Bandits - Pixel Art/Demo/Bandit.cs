using System;
using UnityEngine;

#pragma warning disable CS0649

public class Bandit : MonoBehaviour {
    public event Action AttackedEvent = delegate { };
    public event Action DiedEvent = delegate { };
    public event Action RecoveredEvent = delegate { };
    public event Action JumpedEvent = delegate { };
    public event Action LandedEvent = delegate { };
    public event Action FootstepEvent = delegate { };


    [SerializeField] float m_speed = 1.0f;
    [SerializeField] float m_jumpForce = 2.0f;

    [SerializeField] Animator m_animator;
    [SerializeField] Rigidbody2D m_body2d;
    [SerializeField] Sensor_Bandit m_groundSensor;
    bool m_grounded;
    bool m_combatIdle;
    bool m_isDead;

    float postAttackCooldown;


    // Update is called once per frame
    void Update() {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            LandedEvent();
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        var inputX = postAttackCooldown <= 0 ? Input.GetAxis("Horizontal") : 0;

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        if (postAttackCooldown > 0)
            postAttackCooldown -= Time.deltaTime;

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e")) {
            if (!m_isDead) {
                m_animator.SetTrigger("Death");
                DiedEvent();
            } else {
                m_animator.SetTrigger("Recover");
                RecoveredEvent();
            }

            m_isDead = !m_isDead;
        }

        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0) && postAttackCooldown <= 0) {
            m_animator.SetTrigger("Attack");
            postAttackCooldown = 0.9f;
            AttackedEvent();
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded) {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
            JumpedEvent();
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    void FireAnimationEventOnFootstep() {
        FootstepEvent();
    }
}
