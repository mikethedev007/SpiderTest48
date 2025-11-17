using SpiderTest48.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain.Dtos
{
    public class GameQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public GameEnum.GameInputType QuestionType { get; set; }
        public bool IsAnswered { get; set; } = false;

        public GameQuestion()
        {
            
        }

    }
}
