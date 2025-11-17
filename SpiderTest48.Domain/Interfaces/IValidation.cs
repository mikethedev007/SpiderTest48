using SpiderTest48.Domain.Dtos;
using SpiderTest48.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain.Interfaces
{
    public interface IValidation
    {
        Result ValidateInput(string input, GameEnum.GameInputType gameInputType);
    }
}
