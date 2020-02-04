using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

namespace Complete
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        public int roundsToWin = 5;
        public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
        public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
        public CameraControl cameraControl;
        public Text bigTextInTheMiddleOfTheScreen;                  // Reference to the overlay Text to display winning text, etc.
        public TankManager[] tankManagers;

        //private
        int currentRoundNumber;
        WaitForSeconds startDelay;
        WaitForSeconds endDelay;
        TankManager roundWinner;  //Used to make an announcement of who won.
        TankManager gameWinner;   //Used to make an announcement of who won.

        string coloredSentinelPlayerText; // the nickname of a player who represents a draw

        private void Awake()
        {
            ConfigureSingleton();
            ConfigureDelays();
            gameWinner = CreateSentinelPlayer();
        }

        void ConfigureSingleton()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        void ConfigureDelays()
        {
            startDelay = new WaitForSeconds(m_StartDelay);
            endDelay = new WaitForSeconds(m_EndDelay);
        }

        // called when enough players join the game, after some seconds delay
        public void Begin()
        {
            UpdateTankManagers();

            SetCameraTargets();

            StartCoroutine(GameLoop());
        }


        public void UpdateTankManagers()
        {
            GameObject[] tanks = GameObject.FindGameObjectsWithTag("tank");
            for (int i = 0; i < tanks.Length; i++)
            {
                tankManagers[i].FullSetupWithTankAndItsNumber(tanks[i], i+1);
            }
        }

        private void SetCameraTargets()
        {
            Transform[] transforms = new Transform[tankManagers.Length];

            for (int i = 0; i < tankManagers.Length; i++)
            {
                transforms[i] = tankManagers[i].GetTankTransform();
            }

            cameraControl.m_Targets = transforms;
        }


        private IEnumerator GameLoop()
        {
            while ( !WeHaveAWinner() )
            {
                yield return StartCoroutine(RoundStarting());

                yield return StartCoroutine(RoundPlaying());

                yield return StartCoroutine(RoundEnding());

            }

            //restart game
            SceneManager.LoadScene(0);
        }

        bool WeHaveAWinner()
        {
            //compare the name of the game winner with the default name of the players that represents draws
            return gameWinner.m_ColoredPlayerText != coloredSentinelPlayerText;
        }

        private IEnumerator RoundStarting()
        {
            ResetAllTanks();

            DisableTanksControls();

            cameraControl.SetStartPositionAndSize();

            IncrementAndShowNumberOfRound();

            yield return startDelay;
        }

        private void ResetAllTanks()
        {
            for (int i = 0; i < tankManagers.Length; i++)
            {
                tankManagers[i].ResetAllTankScripts();
            }
        }

        private void DisableTanksControls()
        {
            for (int i = 0; i < tankManagers.Length; i++)
            {
                tankManagers[i].DisableControl();
            }
        }

        void IncrementAndShowNumberOfRound()
        {
            currentRoundNumber++;
            bigTextInTheMiddleOfTheScreen.text = "ROUND " + currentRoundNumber;
        }

        private IEnumerator RoundPlaying()
        {
            EnableAllTanksControls();

            bigTextInTheMiddleOfTheScreen.text = string.Empty;

            while (MoreThanOneTankAlive())
            {
                yield return null;
            }
        }

        private void EnableAllTanksControls()
        {
            for (int i = 0; i < tankManagers.Length; i++)
            {
                tankManagers[i].EnableControl();
            }
        }

        private bool MoreThanOneTankAlive()
        {
            int numTanksAlive = tankManagers.Length;

            for (int i = 0; i < tankManagers.Length; i++)
            {
                if (tankManagers[i].IsDead())
                    numTanksAlive--;
            }

            return numTanksAlive > 1;
        }

        private IEnumerator RoundEnding()
        {
            DisableTanksControls();

            roundWinner = GetRoundWinner();
            roundWinner.m_Wins++;

            gameWinner = GetGameWinner();

            DisplayEndMessage();

            yield return endDelay;
        }

        // This function is called with the assumption that 1 or fewer tanks are currently alive.
        private TankManager GetRoundWinner()
        {
            for (int i = 0; i < tankManagers.Length; i++)
            {
                if ( ! tankManagers[i].IsDead() )
                    return tankManagers[i];
            }

            return CreateSentinelPlayer();
        }

        private TankManager GetGameWinner()
        {
            for (int i = 0; i < tankManagers.Length; i++)
            {
                if (tankManagers[i].m_Wins == roundsToWin)
                    return tankManagers[i];
            }

            return CreateSentinelPlayer();
        }

        TankManager CreateSentinelPlayer()
        {
            TankManager sentinelPlayer = new TankManager();
            sentinelPlayer.m_ColoredPlayerText = coloredSentinelPlayerText;
            return sentinelPlayer;
        }

        void DisplayEndMessage()
        {
            EndTextMaker endTextMaker = new EndTextMaker(tankManagers, gameWinner,
                                                    roundWinner, coloredSentinelPlayerText);
            string message = endTextMaker.EndMessage();
            bigTextInTheMiddleOfTheScreen.text = message;
        }

    }

}