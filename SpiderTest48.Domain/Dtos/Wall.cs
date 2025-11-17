using SpiderTest48.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain.Dtos
{
    public class Wall
    {
        public Position Dimension { get; set; } = new Position(10, 10, GameEnum.Orientation.None);
        public List<WallPosition> WallGridPositions { get; set; } = new List<WallPosition>();
        public Wall() 
        {
            InitializeWallGridPositions();
        }

        public Wall(Position position)
        {
            Dimension = position;
            InitializeWallGridPositions();
        }


        public void InitializeWallGridPositions()
        {
            WallGridPositions.Clear();
            for (int x = 0; x < Dimension.X; x++)
            {
                for (int y = 0; y < Dimension.Y; y++)
                {
                    WallGridPositions
                        .Add( new WallPosition(new Position(x, y, Enums.GameEnum.Orientation.None), false));
                }
            }
        }

    }

    //public class WallPosition
    //{
    //    public Position Position { get; set; }
    //    public bool IsOccupied { get; set; } = false;
    //    public WallPosition()
    //    {
    //    }
    //    public WallPosition(Position position, bool isOccupied)
    //    {
    //        Position = position;
    //        IsOccupied = isOccupied;
    //    }
    //}
}
