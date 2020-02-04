using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class EndTextMaker
    {
        TankManager gameWinner, roundWinner;
        string drawColoredPlayerText;
        TankManager[] tankManagers;

        public EndTextMaker(TankManager[] tankManagers, TankManager m_GameWinner,
                            TankManager m_RoundWinner, string drawColoredPlayerText)
        {
            this.tankManagers = tankManagers;
            this.gameWinner = m_GameWinner;
            this.roundWinner = m_RoundWinner;
            this.drawColoredPlayerText = drawColoredPlayerText;
        }

        // Returns a string message to display at the end of each round.
        public string EndMessage()
        {
            string message;

            if (WeHaveAWinner())
            {
                message = WriteWinnerText();
            }
            else
            {
                message = WriteRoundWinnerAndPlayersScores();
            }

            return message;
        }

        bool WeHaveAWinner()
        {
            return gameWinner.m_ColoredPlayerText != drawColoredPlayerText;
        }

        string WriteWinnerText()
        {
            return gameWinner.m_ColoredPlayerText + " WINS THE GAME!";
        }

        string WriteRoundWinnerAndPlayersScores()
        {
            string message = WriteWhoWonTheRound();

            message += WriteLineBreaks();

            for (int i = 0; i < tankManagers.Length; i++)
            {
                message += WritePlayersScore(i);
            }

            return message;
        }

        string WriteWhoWonTheRound()
        {
            return roundWinner.m_ColoredPlayerText + " WINS THE ROUND!";
        }

        string WriteLineBreaks()
        {
            return "\n\n\n\n";
        }

        string WritePlayersScore(int i)
        {
            return tankManagers[i].m_ColoredPlayerText + ": " + tankManagers[i].m_Wins + " WINS\n";
        }

    }
}
