using UnityEngine;

namespace Complete
{
    public class TankMovement : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
        public float m_Speed = 12f;                 // How fast the tank moves forward and back.
        public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.

        private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
        private string m_TurnAxisName;              // The name of the input axis for turning.
        [SerializeField] Rigidbody m_Rigidbody;
        private float m_MovementInputValue;         // The current value of the movement input.
        private float m_TurnInputValue;             // The current value of the turn input.
        private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Tanks

        public void setm_MovementInputValue(float inpt)
        {
            m_MovementInputValue = inpt;
        }

        public void setm_TurnInputValue(float inpt)
        {
            m_TurnInputValue = inpt;
        }

        public void StopTankAndTrailOfSmoke()
        {
            m_Rigidbody.velocity = Vector3.zero;

            ResetInputValues();

            ResetParticleSystems();
        }

        void ResetInputValues()
        {
            m_MovementInputValue = 0f;
            m_TurnInputValue = 0f;
        }

        void ResetParticleSystems()
        {
            m_particleSystems = GetComponentsInChildren<ParticleSystem>();
            
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Stop();
                m_particleSystems[i].Play();
            }
        }

        private void FixedUpdate ()
        {
            // Adjust the rigidbodies position and orientation in FixedUpdate.
            Move ();
            Turn ();
        }


        private void Move ()
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        }


        private void Turn ()
        {
            // Determine the number of degrees to be turned based on the input, speed and time between frames.
            float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
        }
    }
}