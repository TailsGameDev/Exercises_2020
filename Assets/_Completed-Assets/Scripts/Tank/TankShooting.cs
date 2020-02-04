using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class TankShooting : Bolt.GlobalEventListener
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.
        public Rigidbody m_Shell;                   // Prefab of the shell.
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
        public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
        public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
        public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
        public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.


        private string m_FireButton = "Fire1";      // The input axis that is used for launching shells.
        private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
        private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        bool shooting = false;

        public void ResetLaunchForceAndUI()
        {
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_MinLaunchForce;
        }


        private void Start ()
        {
            // The rate that the launch force charges up is the range of possible forces by the max charge time.
            m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        }

        public void ChargeShoot()
        {
            if (!shooting)
            {
                StartCoroutine(Shoot());
            }
        }

        public void ReleaseShoot()
        {
            shooting = false;
        }

        IEnumerator Shoot()
        {
            shooting = true;
            // inicialize charge
            m_CurrentLaunchForce = m_MinLaunchForce;

            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();
            
            // charge loop
            while( shooting && (m_CurrentLaunchForce < m_MaxLaunchForce))
            {
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

                m_AimSlider.value = m_CurrentLaunchForce;

                yield return null;
            }

            shooting = false;

            //Fire and end
            m_CurrentLaunchForce = Mathf.Min(m_CurrentLaunchForce, m_MaxLaunchForce);

            Fire();

            m_AimSlider.value = m_MinLaunchForce;
        }

        private void Fire ()
        {
            // Create an instance of the shell and store a reference to it's rigidbody.
            var shellInstance =
                BoltNetwork.Instantiate(
                    BoltPrefabs.BoltShell,
                    m_FireTransform.position,
                    m_FireTransform.rotation
                );

            Rigidbody shellRigidbody = shellInstance.GetComponent<Rigidbody>();
            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellRigidbody.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play ();

            // Reset the launch force.  This is a precaution in case of missing button events.
            m_CurrentLaunchForce = m_MinLaunchForce;
        }
    }
}