using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B17_Ex05.BullsAndCowsWinApp
{
    public class WinColorPalette : Form
    {
        private const int k_NumberOfColorsToPick = 8;
        private const int k_SpaceFromLeft = 40;
        private const int k_SpaceFromTop = 20;
        private const int k_ButtonSize = 40;
        private const int k_SpaceSize = 10;
        private const int k_SpaceToGuessAnswer = 30;
        private readonly Color[] r_ArrayOfColor = new Color[] { Color.MediumPurple, Color.Aquamarine, Color.LightGreen, Color.Red, Color.Blue, Color.Yellow, Color.Coral, Color.DeepPink };
        private readonly Button m_ParentButton;

        public WinColorPalette(Button i_CurrentButton)
        {
            this.Size = new Size(300, 170);
            this.Text = "Choose a color";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.m_ParentButton = i_CurrentButton;
            for (int i = 0; i < k_NumberOfColorsToPick; i++)
            {
                Button ChosenColor = new Button();

                ChosenColor.Size = new Size(k_ButtonSize, k_ButtonSize);
                ChosenColor.BackColor = r_ArrayOfColor[i];
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
                this.MaximizeBox = false;
                ChosenColor.Click += new EventHandler(ChosenColor_Clicked);
                ChosenColor.Location = new Point(k_SpaceFromLeft + ((k_ButtonSize + k_SpaceSize) * (i / 2)), ((k_ButtonSize + k_SpaceSize) * (i % 2)) + k_SpaceFromTop);
                this.Controls.Add(ChosenColor);
            }
        }

        private void ChosenColor_Clicked(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;

            this.m_ParentButton.BackColor = currentButton.BackColor;
            this.Close();
        }
    }
}
