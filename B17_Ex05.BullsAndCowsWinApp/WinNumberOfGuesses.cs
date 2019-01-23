using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using B17_Ex05.BullsAndCowsGameLogic;

namespace B17_Ex05.BullsAndCowsWinApp
{
    public class WinNumberOfGuesses : Form
    {
        private readonly Button m_StartButton = new Button();
        private readonly Button m_NmberOfGuesses = new Button();
        private readonly GameLogic m_LogicGame = new GameLogic();

        public WinNumberOfGuesses()
        {
            this.Size = new Size(292, 160);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Text = "Bulls And Cow";
            InitializeStartButton();
            InitializeNmberOfGuessesButton();
        }

        public void InitializeNmberOfGuessesButton()
        {
            m_NmberOfGuesses.Text = "Number of guesses: " + m_LogicGame.CurrentMaxNumberOfGuesses;
            m_NmberOfGuesses.Location = new Point(16, 10);
            m_NmberOfGuesses.Size = new Size(246, 20);
            this.Controls.Add(this.m_NmberOfGuesses);
            m_NmberOfGuesses.Click += new EventHandler(NumberOfGuessesButton_Clicked);
        }

        public void InitializeStartButton()
        {
            m_StartButton.Text = "Start";
            m_StartButton.Location = new Point(182, 85);
            m_StartButton.Size = new Size(76, 28);
            this.Controls.Add(this.m_StartButton);
            m_StartButton.Click += new EventHandler(StartButton_Clicked);
        }

        public void NumberOfGuessesButton_Clicked(object sender, EventArgs e)
        {
            if (m_LogicGame.CurrentMaxNumberOfGuesses != m_LogicGame.MaxNumberOfGuesses)
            {
                m_LogicGame.CurrentMaxNumberOfGuesses++;
                m_NmberOfGuesses.Text = "Number of guesses: " + m_LogicGame.CurrentMaxNumberOfGuesses;
            }
            else
            {
                m_LogicGame.CurrentMaxNumberOfGuesses = m_LogicGame.MinNumberOfGuesses;
                m_NmberOfGuesses.Text = "Number of guesses: " + m_LogicGame.CurrentMaxNumberOfGuesses;
            }
        }

        public void StartButton_Clicked(object sender, EventArgs e)
        {
            WinBoard GameBoard = new WinBoard(m_LogicGame.CurrentMaxNumberOfGuesses);
            this.Close();
            GameBoard.ShowDialog();
        }
    }
}
