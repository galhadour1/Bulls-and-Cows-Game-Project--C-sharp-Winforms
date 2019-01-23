using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using B17_Ex05.BullsAndCowsGameLogic;

namespace B17_Ex05.BullsAndCowsWinApp
{
    public class WinBoard : Form
    {
        private const ushort k_SquareSize = 40;
        private const int k_SpaceFromLeft = 40;
        private const int k_SpaceFromBlackButton = 40;
        private const int k_SpaceFromTop = 20;
        private const int k_SpaceSize = 10;
        private const int k_GuessAnswerSpace = 6;
        private const int k_SpaceToGuessAnswer = 30;
        private const int k_GuessAnswerSize = 17;
        private const int k_ApproveButtonHeight = k_SquareSize / 2;
        private const int k_ApproveButtonHeightOffset = k_ApproveButtonHeight / 2;
        private readonly List<Button> m_Buttons = new List<Button>();
        private readonly List<string> m_UserColorsGuess = new List<string>();
        private readonly List<Button> m_GuessResultButtons = new List<Button>();
        private readonly Program m_RestartTheGame = new Program();
        private readonly GameLogic m_LogicGame = new GameLogic();
        private bool[] m_CurrentRowGuessed;
        private int m_NumberOfCurrentRowGuesses = 0;
        private Button[] m_ComputerSequenceButtons;
        private int m_GuessesLeft;
        private StringBuilder m_CodeToGuessString = new StringBuilder();

        public WinBoard(ushort i_CurrentMaxNumberOfGuesses)
        {
            m_LogicGame.SetBoard(i_CurrentMaxNumberOfGuesses);
            this.Size = new Size(380, m_LogicGame.CurrentMaxNumberOfGuesses * (94 - (m_LogicGame.CurrentMaxNumberOfGuesses * 2)));
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Text = "Bulls And Cow Game Board";
            m_GuessesLeft = i_CurrentMaxNumberOfGuesses;
            m_LogicGame.GetComputerSequence();
            InitializeComputerSequenceButtons();
            InitializeGuessesOfUserButtons();
        }

        public void InitializeComputerSequenceButtons()
        {
            m_ComputerSequenceButtons = new Button[m_LogicGame.LengthOfSequence];
            for (int i = 0; i < m_LogicGame.LengthOfSequence; ++i)
            {
                m_ComputerSequenceButtons[i] = new Button();
                m_ComputerSequenceButtons[i].BackColor = Color.Black;
                m_ComputerSequenceButtons[i].Size = new Size(k_SquareSize, k_SquareSize);
                m_ComputerSequenceButtons[i].Enabled = false;
                m_ComputerSequenceButtons[i].Location = new Point(k_SpaceFromLeft + ((k_SquareSize + k_SpaceSize) * i), k_SpaceFromTop);
                this.Controls.Add(m_ComputerSequenceButtons[i]);
            }
        }

        public void InitializeGuessesOfUserButtons()
        {
            Button[] sequenceOfUserButton = new Button[m_LogicGame.LengthOfSequence];
            int totalSpaceFromBlackButton = k_SpaceFromTop + k_SquareSize + k_SpaceFromBlackButton;

            for (int i = 0; i < m_LogicGame.CurrentMaxNumberOfGuesses; i++)
            {
                for (int j = 0; j < m_LogicGame.LengthOfSequence; j++)
                {
                    sequenceOfUserButton[j] = new Button();
                    sequenceOfUserButton[j].Size = new Size(k_SquareSize, k_SquareSize);
                    if (i != 0)
                    {
                        sequenceOfUserButton[j].Enabled = false;
                    }

                    sequenceOfUserButton[j].Name = string.Format("R{0}C{1}", i, j);
                    sequenceOfUserButton[j].Location = new Point(k_SpaceFromLeft + ((k_SquareSize + k_SpaceSize) * j), ((k_SquareSize + k_SpaceSize) * i) + totalSpaceFromBlackButton);
                    sequenceOfUserButton[j].Click += new EventHandler(SequenceOfUserButton_Clicked);
                    m_Buttons.Add(sequenceOfUserButton[j]);
                    this.Controls.Add(sequenceOfUserButton[j]);
                }

                InitializeConfirmSequenceButtons(i, totalSpaceFromBlackButton);
                InitializeResultButtons(i, totalSpaceFromBlackButton);
            }
        }

        public void InitializeConfirmSequenceButtons(int i_IndexRow, int i_TotalSpaceFromBlackButton)
        {
            Button confirmSequenceButton = new Button();

            confirmSequenceButton.Name = string.Format("R{0}", i_IndexRow);
            confirmSequenceButton.Text = "-->>";
            confirmSequenceButton.Enabled = false;
            confirmSequenceButton.Size = new Size(k_SquareSize, k_ApproveButtonHeight);
            confirmSequenceButton.Location = new Point(k_SpaceFromLeft + ((k_SquareSize + k_SpaceSize) * m_LogicGame.LengthOfSequence), ((k_SquareSize + k_SpaceSize) * i_IndexRow) + k_ApproveButtonHeightOffset + i_TotalSpaceFromBlackButton);
            confirmSequenceButton.Click += new EventHandler(ConfirmSequenceButton_Clicked);
            m_Buttons.Add(confirmSequenceButton);
            this.Controls.Add(confirmSequenceButton);
        }

        public void InitializeResultButtons(int i_IndexRow, int i_TotalSpaceFromBlackButton)
        {
            for (int i = 0; i < m_LogicGame.LengthOfSequence; i++)
            {
                Button guessResultButtons = new Button();
                guessResultButtons.Enabled = false;
                guessResultButtons.Size = new Size(k_GuessAnswerSize, k_GuessAnswerSize);
                guessResultButtons.Location = new Point(k_SpaceFromLeft + ((k_SquareSize + k_SpaceSize) * m_LogicGame.LengthOfSequence) + k_SquareSize + k_SpaceToGuessAnswer + ((k_GuessAnswerSize + k_GuessAnswerSpace) * (i / 2)), ((k_SquareSize + k_SpaceSize) * i_IndexRow) + ((k_GuessAnswerSize + k_GuessAnswerSpace) * (i % 2)) + i_TotalSpaceFromBlackButton);
                m_GuessResultButtons.Add(guessResultButtons);
                this.Controls.Add(guessResultButtons);
            }
        }

        public void SequenceOfUserButton_Clicked(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            int indexOfC = currentButton.Name.IndexOf("C");
            int buttonRow = -1;
            int buttonCol = -1;

            m_CurrentRowGuessed = new bool[m_LogicGame.LengthOfSequence];
            int.TryParse(currentButton.Name.Substring(1, indexOfC - 1), out buttonRow);
            int.TryParse(currentButton.Name.Substring(indexOfC + 1, currentButton.Name.Length - indexOfC - 1), out buttonCol);
            if (m_CurrentRowGuessed[buttonCol] == false)
            {
                m_CurrentRowGuessed[buttonCol] = true;
                m_NumberOfCurrentRowGuesses++;
            }

            if (m_NumberOfCurrentRowGuesses == m_LogicGame.LengthOfSequence)
            {
                m_Buttons[(buttonRow * (m_LogicGame.LengthOfSequence + 1)) + m_LogicGame.LengthOfSequence].Enabled = true;
            }

            WinColorPalette colorPalette = new WinColorPalette(currentButton);
            colorPalette.ShowDialog();
        }

        public void ConfirmSequenceButton_Clicked(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            int buttonRow = -1;
            bool isWon = false;

            int.TryParse(currentButton.Name.Substring(1, currentButton.Name.Length - 1), out buttonRow);
            EnabledButtons(buttonRow);
            UpdateColorsGuess(buttonRow);
            currentButton.Enabled = false;
            m_LogicGame.CheckResults(m_UserColorsGuess);
            m_GuessesLeft--;
            UpdateBullsAndCows(buttonRow);
            isWon = m_LogicGame.IsWon;
            UserWon(isWon);
            UserLost(isWon);
            m_LogicGame.ClearResult(m_UserColorsGuess);
        }

        private void ShowComputerSequence()
        {
            for (int i = 0; i < m_LogicGame.LengthOfSequence; i++)
            {
                Color CurrentCodeColor = Color.FromName(m_LogicGame.GetComputerSequenceByIndex(i));

                m_ComputerSequenceButtons[i].BackColor = CurrentCodeColor;
            }
        }

        public void UpdateColorsGuess(int i_ButtonRow)
        {
            for (int i = 0; i < m_LogicGame.LengthOfSequence; i++)
            {
                m_UserColorsGuess.Add(m_Buttons[i + ((m_LogicGame.LengthOfSequence + 1) * i_ButtonRow)].BackColor.Name);
            }
        }

        public void EnabledButtons(int i_ButtonRow)
        {
            for (int i = 0; i < m_LogicGame.LengthOfSequence; i++)
            {
                m_Buttons[i + ((m_LogicGame.LengthOfSequence + 1) * i_ButtonRow)].Enabled = false;
                m_CurrentRowGuessed[i] = false;
                if (i_ButtonRow != (m_LogicGame.CurrentMaxNumberOfGuesses - 1))
                {
                    m_Buttons[i + ((m_LogicGame.LengthOfSequence + 1) * (i_ButtonRow + 1))].Enabled = true;
                }
            }

            m_NumberOfCurrentRowGuesses = 0;
        }

        public void UpdateBullsAndCows(int i_ButtonRow)
        {
            for (int i = 0; i < m_LogicGame.Bulls; i++)
            {
                m_GuessResultButtons[i + (m_LogicGame.LengthOfSequence * i_ButtonRow)].BackColor = Color.Black;
            }

            for (int i = m_LogicGame.Bulls; i < m_LogicGame.LengthOfSequence; i++)
            {
                if (m_LogicGame.Cows > 0)
                {
                    m_GuessResultButtons[i + (m_LogicGame.LengthOfSequence * i_ButtonRow)].BackColor = Color.Yellow;
                    m_LogicGame.Cows--;
                }
            }
        }

        public void UserWon(bool i_IsWon)
        {
            if (i_IsWon)
            {
                ShowComputerSequence();
                DialogResult restartGame = MessageBox.Show("Good job, You won the game!" + Environment.NewLine + "Do you want to play again?", "Game Won", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (restartGame == DialogResult.Yes)
                {
                    this.Close();
                    m_RestartTheGame.m_IsYesClicked = true;
                    m_RestartTheGame.RunGame();
                }
                else if (restartGame == DialogResult.No)
                {
                    Environment.Exit(0);
                }
            }
        }

        public void UserLost(bool i_IsWon)
        {
            if ((m_GuessesLeft == 0) && (!i_IsWon))
            {
                m_CodeToGuessString = m_LogicGame.ConvertTheCodeToString();
                ShowComputerSequence();
                DialogResult restartGame = MessageBox.Show("Bad luck, Try again next time" + Environment.NewLine + "The code is:" + m_CodeToGuessString + Environment.NewLine + "Do you want to play again?", "Game Lose", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (restartGame == DialogResult.Yes)
                {
                    this.Close();
                    m_RestartTheGame.m_IsYesClicked = true;
                    m_RestartTheGame.RunGame();
                }
                else if (restartGame == DialogResult.No)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
