using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B17_Ex05.BullsAndCowsGameLogic
{
    public class GameLogic
    {
        private const ushort k_LengthSequence = 4;
        private const int k_NumberOfColorsToChoose = 8;
        private const ushort k_MaxNumberOfGuesses = 10;
        private const ushort k_MinNumberOfGuesses = 4;
        private readonly List<string> r_ComputerSequence = new List<string>(k_LengthSequence);
        private readonly List<string> r_ColorsToChoose = new List<string> { "MediumPurple", "Aquamarine", "LightGreen", "Red", "Blue", "Yellow", "Coral", "DeepPink" };
        private string[] m_Guesses;
        private string[] m_Score;
        private ushort m_CurrentMaxNumberOfGuesses = k_MinNumberOfGuesses;
        private ushort m_IndexGusses;
        private bool m_IsWon = false;
        private int m_Bulls = 0;
        private int m_Cows = 0;

        public ushort CurrentMaxNumberOfGuesses
        {
            get
            {
                return m_CurrentMaxNumberOfGuesses;
            }

            set
            {
                m_CurrentMaxNumberOfGuesses = value;
            }
        }

        public bool IsWon
        {
            get
            {
                return m_IsWon;
            }
        }

        public int Bulls
        {
            get
            {
                return m_Bulls;
            }
        }

        public int Cows
        {
            get
            {
                return m_Cows;
            }

            set
            {
                m_Cows = value;
            }
        }

        public ushort LengthOfSequence
        {
            get
            {
                return k_LengthSequence;
            }
        }

        public ushort MinNumberOfGuesses
        {
            get
            {
                return k_MinNumberOfGuesses;
            }
        }

        public ushort MaxNumberOfGuesses
        {
            get
            {
                return k_MaxNumberOfGuesses;
            }
        }

        public string[] Guesses
        {
            get
            {
                return m_Guesses;
            }
        }

        public string[] Score
        {
            get
            {
                return m_Score;
            }
        }

        public string Guesse
        {
            set
            {
                m_Guesses[m_IndexGusses] = value;
            }
        }

        public ushort IndexGusses
        {
            get
            {
                return m_IndexGusses;
            }
        }

        public List<string> ComputerSequence
        {
            get
            {
                return r_ComputerSequence;
            }
        }

        public void SetBoard(ushort i_MaxNumOfGuesses)
        {
            m_CurrentMaxNumberOfGuesses = i_MaxNumOfGuesses;
            m_Guesses = new string[m_CurrentMaxNumberOfGuesses];
            m_Score = new string[m_CurrentMaxNumberOfGuesses];            m_IndexGusses = 0;

            for (int i = 0; i < m_CurrentMaxNumberOfGuesses; i++)
            {
                m_Guesses[i] = null;
                m_Score[i] = null;
            }
        }

        public string GetComputerSequenceByIndex(int i_Index)
        {
            return r_ComputerSequence[i_Index];
        }

        public void GetComputerSequence()
        {
            int checkDoubleColor;
            Random rand = new Random();
            bool[] chosenColors = new bool[k_NumberOfColorsToChoose];

            // initialization of 'chosenColors'
            for (int i = 0; i < k_NumberOfColorsToChoose; i++)
            {
                chosenColors[i] = false;
            }

            for (int i = 0; i < k_LengthSequence; i++)
            {
                do
                {
                    checkDoubleColor = rand.Next(0, k_NumberOfColorsToChoose);
                }
                while (chosenColors[checkDoubleColor]); // the color is already in use
                chosenColors[checkDoubleColor] = true;
                r_ComputerSequence.Add(r_ColorsToChoose[checkDoubleColor]);
            }
        }

        public void CheckResults(List<string> i_SequenceFromUser)
        {
            List<string> results = new List<string>(k_LengthSequence);

            for (int i = 0; i < k_LengthSequence; i++)
            {
                for (int j = 0; j < k_LengthSequence; j++)
                {
                    if (i_SequenceFromUser[i] == r_ComputerSequence[j])
                    {
                        if (i == j)
                        {
                            m_Bulls++;
                        }
                        else
                        {
                            m_Cows++;
                        }
                    }
                }
            }

            if (m_Bulls == k_LengthSequence)
            {
                m_IsWon = true;
            }
        }

        public StringBuilder ConvertTheCodeToString()
        {
            StringBuilder codeToGuessString = new StringBuilder();

            for (int i = 0; i < r_ComputerSequence.Count; i++)
            {
                codeToGuessString.Append(r_ComputerSequence[i]);
                codeToGuessString.Append(",");
            }

            return codeToGuessString;
        }

        public void ClearResult(List<string> i_UserGuess)
        {
            m_Bulls = 0;
            m_Cows = 0;
            for (int i = i_UserGuess.Count - 1; i >= 0; i--)
            {
                i_UserGuess.Remove(i_UserGuess[i]);
            }
        }
    }
}