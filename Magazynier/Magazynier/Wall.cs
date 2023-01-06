using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazynier
{
    class Wall
    {
        private Point start;
        private Point end;

        public Point Start { get => start; }
        public Point End { get => end;  }

        public Wall(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
