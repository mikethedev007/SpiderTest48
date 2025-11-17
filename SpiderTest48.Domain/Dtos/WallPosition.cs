using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain.Dtos
{
    public class WallPosition
    {
        public Position Position { get; set; }
        public bool IsOccupied { get; set; } = false;
        public WallPosition()
        {
        }
        public WallPosition(Position position, bool isOccupied)
        {
            Position = position;
            IsOccupied = isOccupied;
        }
    }
}
