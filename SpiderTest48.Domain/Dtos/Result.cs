using SpiderTest48.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain.Dtos
{
    public class Result
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }
        public GameEnum.GameStatus Status { get; set; } = GameEnum.GameStatus.None;
        public Result() { }
    }
}
