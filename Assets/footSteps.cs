using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class footSteps : MonoBehaviour
{
    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
    [SerializeField] private float m_StepInterval;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    [SerializeField] private ActionBasedContinuousMoveProvider moveProvider;
    
    [SerializeField] private InputActionReference a;

    private CharacterController m_CharacterController;
    private float m_StepCycle;
    private float m_NextStep;
    private AudioSource m_AudioSource;

    bool isWalking = false;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = a.action.ReadValue<Vector2>();

        float speed = 0;

        if(movement.x > 0 || movement.y > 0)
        {
            isWalking = true;

            if(movement.x > movement.y)
            {
                speed = (moveProvider.moveSpeed) * (movement.x);
            }
            else
            {
                speed = (moveProvider.moveSpeed) * (movement.y);
            }
            

        }
        else if(movement.x < 0 || movement.y < 0)
        {
            isWalking = true;

            if (movement.x < movement.y)
            {
                speed = (moveProvider.moveSpeed) * (Mathf.Abs(movement.x));
            }
            else
            {
                speed = (moveProvider.moveSpeed) * (Mathf.Abs(movement.y));
            }
        }
        else
        {
            isWalking = false;
        }

        if (isWalking == true)
        {
            ProgressStepCycle(speed);
        }
    }

    private void ProgressStepCycle(float speed)
    {
        if (m_CharacterController.velocity.sqrMagnitude > 0)
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                         Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + m_StepInterval;

        PlayFootStepAudio();
    }


    private void PlayFootStepAudio()
    {
        if (!m_CharacterController.isGrounded)
        {
            return;
        }
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }
}
