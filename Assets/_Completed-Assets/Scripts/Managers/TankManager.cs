using System;
using UnityEngine;

namespace Complete
{
    [Serializable]
    public class TankManager
    {
        // This class
        // works with the GameManager
        // and configures whether or not players have control of their tank in the 
        // different phases of the game.

        public Color m_PlayerColor;
        public Transform m_SpawnPoint;
        [HideInInspector] public int m_PlayerNumber;
        [HideInInspector] public string m_ColoredPlayerText;
        [HideInInspector] public GameObject m_Instance;
        [HideInInspector] public int m_Wins;
        

        private TankMovement tankMovement; // used to disable and enable control.
        private TankShooting tankShooting; // used to disable and enable control.
        private TankHealth tankHealth;

        // Used to disable the world space UI during the Starting and Ending phases of each round.
        private GameObject m_CanvasGameObject;

        public bool IsDead()
        {
            return tankHealth.IsDead();
        }

        public void FullSetupWithTankAndItsNumber (GameObject tankGameObject, int tankNumber)
        {
            m_Instance = tankGameObject;
            m_PlayerNumber = tankNumber;

            // Get references to the components.
            tankMovement = m_Instance.GetComponent<TankMovement>();
            tankShooting = m_Instance.GetComponent<TankShooting>();
            tankHealth = m_Instance.GetComponent<TankHealth>();
            m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas> ().gameObject;

            // Set the player numbers to be consistent across the scripts.
            tankMovement.m_PlayerNumber = m_PlayerNumber;
            tankShooting.m_PlayerNumber = m_PlayerNumber;

            // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
            m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

            // Get all of the renderers of the tank.
            MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer> ();

            // Go through all the renderers...
            for (int i = 0; i < renderers.Length; i++)
            {
                // ... set their material color to the color specific to this tank.
                renderers[i].material.color = m_PlayerColor;
            }
        }

        public void DisableControl ()
        {
            tankMovement.enabled = false;
            tankShooting.enabled = false;

            m_CanvasGameObject.SetActive (false);
        }

        public void EnableControl ()
        {
            tankMovement.enabled = true;
            tankShooting.enabled = true;

            m_CanvasGameObject.SetActive (true);
        }

        public void ResetAllTankScripts ()
        {
            ResetTransform();

            tankShooting.ResetLaunchForceAndUI();

            tankHealth.HealTankAndUpdateUI();

            tankMovement.StopTankAndTrailOfSmoke();
        }

        void ResetTransform()
        {
            m_Instance.transform.position = m_SpawnPoint.position;
            m_Instance.transform.rotation = m_SpawnPoint.rotation;
        }

        public Transform GetTankTransform()
        {
            return m_Instance.transform;
        }
    }
}