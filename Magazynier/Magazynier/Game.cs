using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazynier
{
    class Game
    {
        private Size size;
        private Point warehouseman;
        private Point box;
        private Point endPoint;
        private List<Wall> walls;

        public Size Size { get => size; }
        public Point Warehouseman { get => warehouseman; }
        public Point Box { get => box; }
        public Point EndPoint { get => endPoint; set => endPoint = value; }
        internal List<Wall> Walls { get => walls; }

        public Game()
        {
            size = new Size(15, 10);
            warehouseman = new Point(1, 1);
            box = new Point(2, 2);

            walls = new List<Wall>();
            walls.Add(new Wall(new Point(0, 0),
                               new Point(0, size.Height - 1)));
            walls.Add(new Wall(new Point(0, 0),
                               new Point(size.Width - 1, 0)));
            walls.Add(new Wall(new Point(0, size.Height - 1),
                               new Point(size.Width - 1, size.Height - 1)));
            walls.Add(new Wall(new Point(size.Width - 1, 0),
                               new Point(size.Width - 1, size.Height - 1)));

            walls.Add(new Wall(new Point(4, 0),
                               new Point(4, size.Height - 4)));

            walls.Add(new Wall(new Point(8, 4),
                               new Point(8, size.Height - 1)));

            endPoint = new Point(size.Width - 2, size.Height - 2);
        }

        public Game(string map)
        {
            int x = 0, y = 0;
            walls = new List<Wall>();
            foreach (char c in map)
            {
                switch (c)
                {
                    case '#':
                        walls.Add(new Wall(new Point(x, y), new Point(x, y)));
                        break;
                    case 'W':
                        warehouseman = new Point(x, y);
                        break;
                    case 'B':
                        box = new Point(x, y);
                        break;
                    case 'E':
                        endPoint = new Point(x, y);
                        break;
                    case '\n':
                        y++;
                        x = -1;
                        break;
                    case '\r':
                        x = -1;
                        break;
                }
                x++;
            }
            size = new Size(x, y + 1);
        }

        internal void Control(Keys keyCode)
        {
            if (!isEndGame())
            {
                Point newBox = Move(keyCode, box);
                Point newWarehouseman = Move(keyCode, warehouseman);
                if (warehouseman != newWarehouseman && newWarehouseman != newBox)
                {
                    warehouseman = newWarehouseman;
                    if (box != newBox && newWarehouseman == box)
                    {
                        box = newBox;
                    }
                }
            }

        }

        public bool isEndGame()
        {
            return box == endPoint;
        }

        private Point Move(Keys keyCode, Point location)
        {
            Point newLocation = location;
            switch (keyCode)
            {
                case Keys.Left:
                    newLocation.X--;
                    break;
                case Keys.Right:
                    newLocation.X++;
                    break;
                case Keys.Up:
                    newLocation.Y--;
                    break;
                case Keys.Down:
                    newLocation.Y++;
                    break;
            }
            if (!walls.Exists(w => w.Start.X <= newLocation.X &&
                                  w.Start.Y <= newLocation.Y &&
                                  newLocation.X <= w.End.X &&
                                  newLocation.Y <= w.End.Y))
            {
                location = newLocation;
            }
            return location;
        }
    }
}
