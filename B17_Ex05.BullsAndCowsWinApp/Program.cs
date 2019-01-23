using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using B17_Ex05.BullsAndCowsGameLogic;

namespace B17_Ex05.BullsAndCowsWinApp
{
    public class Program
    {
        internal bool m_IsYesClicked = false;

        [STAThread]
        public static void Main()
        {
            Program BullsAndCows = new Program();
            BullsAndCows.RunGame();
        }

        public void RunGame()
        {
            do
            {
                m_IsYesClicked = false;
                WinNumberOfGuesses form = new WinNumberOfGuesses();
                form.ShowDialog();
            }
            while (m_IsYesClicked);
        }
    }
}
