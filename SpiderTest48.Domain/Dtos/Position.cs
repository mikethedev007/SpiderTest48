using SpiderTest48.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain.Dtos
{
    public class Position
    {
        public int X {  get; set; }
        public int Y { get; set; }

        public GameEnum.Orientation Orientation { get; set; } = GameEnum.Orientation.None;

        public Position()
        {
            
        }

        public Position(int x, int y, GameEnum.Orientation orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }
    }
}
