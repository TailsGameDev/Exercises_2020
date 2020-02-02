using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAudio : MonoBehaviour
{

    [SerializeField] float m_PitchRange = 0.2f;
    [SerializeField] AudioClip m_EngineDriving;
    [SerializeField] AudioClip m_EngineIdling;
    [SerializeField] AudioSource m_MovementAudio;

    float m_MovementInputValue;
    float m_TurnInputValue;
    float m_OriginalPitch;

    public void setm_MovementInputValue(float inpt)
    {
        m_MovementInputValue = inpt;
    }

    public void setm_TurnInputValue(float inpt)
    {
        m_TurnInputValue = inpt;
    }

    private void Start()
    {
        m_OriginalPitch = m_MovementAudio.pitch;
    }

    private void Update()
    {
        EngineAudio();
    }

    private void EngineAudio()
    {
        // If there is no input (the tank is stationary)...
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            // ... and if the audio source is currently playing the driving clip...
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                // ... change the clip to idling and play it.
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            // Otherwise if the tank is moving and if the idling clip is currently playing...
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                // ... change the clip to driving and play.
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }

}
