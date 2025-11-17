using SpiderTest48.Domain.Dtos;
using SpiderTest48.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain.Interfaces
{
    public interface IGame
    {
        Result ProcessQuestion(GameQuestion gameQuestion);
        Result ProcessInstructions(string input);
        void DisplayWallGrid();
        void ResetWallGridPositions(Position dimension);
        void UpdateSpiderCurrentPosition(Position position);
    }
}
