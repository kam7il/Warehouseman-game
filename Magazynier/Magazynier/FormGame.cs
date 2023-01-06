using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazynier
{
    public partial class FormGame : Form
    {
        const int fildSize = 40;
        Graphics g;

        Game myGame;

        public FormGame()
        {
            InitializeComponent();
            //myGame = new Game();
            myGame = new Game(global::Magazynier.Properties.Resources.map1);
            pictureBoxVisualization.Image = new Bitmap(myGame.Size.Width * fildSize,
                                                       myGame.Size.Height * fildSize);
            pictureBoxVisualization.Size = new Size(myGame.Size.Width * fildSize,
                                                    myGame.Size.Height * fildSize);
            g = Graphics.FromImage(pictureBoxVisualization.Image);

            PaintWalls();
            PaintGameState();
        }

        private void PaintWalls()
        {
            g.Clear(BackColor);
            foreach (Wall w in myGame.Walls)
            {
                g.FillRectangle(new SolidBrush(Color.Brown),
                                w.Start.X * fildSize,
                                w.Start.Y * fildSize,
                                (w.End.X - w.Start.X) * fildSize + fildSize,
                                (w.End.Y - w.Start.Y) * fildSize + fildSize);
            }
            g.FillRectangle(new SolidBrush(Color.Black),
                               myGame.EndPoint.X * fildSize,
                               myGame.EndPoint.Y * fildSize,
                               fildSize,
                               fildSize);
            pictureBoxVisualization.Refresh();
        }

        private void PaintGameState()
        {
            g.FillRectangle(new SolidBrush(Color.Green),
                               myGame.Warehouseman.X * fildSize,
                               myGame.Warehouseman.Y * fildSize,
                               fildSize,
                               fildSize);
            g.FillRectangle(new SolidBrush(Color.Yellow),
                               myGame.Box.X * fildSize,
                               myGame.Box.Y * fildSize,
                               fildSize,
                               fildSize);
            pictureBoxVisualization.Refresh();
        }

        private void ClearGameState()
        {
            g.FillRectangle(new SolidBrush(BackColor),
                               myGame.Warehouseman.X * fildSize,
                               myGame.Warehouseman.Y * fildSize,
                               fildSize,
                               fildSize);
            g.FillRectangle(new SolidBrush(BackColor),
                               myGame.Box.X * fildSize,
                               myGame.Box.Y * fildSize,
                               fildSize,
                               fildSize);
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            ClearGameState();
            myGame.Control(e.KeyCode);

            PaintGameState();
            if (myGame.isEndGame())
            {
                myGame = new Game(global::Magazynier.Properties.Resources.map2);
                PaintWalls();
                PaintGameState();
            }
        }
    }
}
