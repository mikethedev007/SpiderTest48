using SpiderTest48.Domain;
using SpiderTest48.Domain.Dtos;
using SpiderTest48.Domain.Enums;
using SpiderTest48.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpiderTest48.MainApp
{
    public class Validation : IValidation
    {
        private readonly string DimensionsInputPattern = @"^(?:[0-9]|1[0-9]|20)\s{1}(?:[0-9]|1[0-9]|20)$";
        private readonly string CurrLocOrientationInputPattern = @"/^(?:[0-9]|1[0-9]|20) {1}(?:[0-9]|1[0-9]|20) {1}(?:Left|Right|Up|Down)$/i";
        private readonly string ExploreInstructionsInputPattern = @"^[FLR]{1,20}$/i";

        public Result ValidateInput(string input, GameEnum.GameInputType gameInputType)
        {
            Result result = new Result();

            string inputString = input.Trim();

            //Check for EXIT
            if (!string.IsNullOrEmpty(input) && input.Trim().ToUpper() == "EXIT")
            {
                result.Message = $"User has entered to Exit Game......";
                result.IsSucess = false;
                result.Status = GameEnum.GameStatus.ExitGame;
                return result;
            }


            switch (gameInputType)
            {
                case GameEnum.GameInputType.Dimensions:
                    if(!IsValidRegex(inputString, DimensionsInputPattern))
                    {
                        result.Message = $"Invalid input for {gameInputType.ToString()}";
                        result.Status |= GameEnum.GameStatus.ProcessingDimensions;
                        result.IsSucess = false;
                        return result;
                    }
                    else 
                    {
                        result.Message = $"Successful validated input for {gameInputType.ToString()}";
                        result.Status |= GameEnum.GameStatus.ProcessingDimensions;
                        result.IsSucess = true;
                    }
                    break;
                case GameEnum.GameInputType.CurrentLocationOrientation:
                    if (!IsValidRegex(inputString, CurrLocOrientationInputPattern))
                    {
                        result.Message = $"Invalid input for {gameInputType.ToString()}";
                        result.IsSucess = false;
                        return result;
                    }
                    else
                    {
                        result.Message = $"Successful validated input for {gameInputType.ToString()}";
                        result.Status |= GameEnum.GameStatus.ProcessingCurrentLocOrientation;
                        result.IsSucess = true;
                    }
                    break;
                case GameEnum.GameInputType.InstructionsForExploration:
                    if (!IsValidRegex(inputString, ExploreInstructionsInputPattern))
                    {
                        result.Message = $"Invalid input for {gameInputType.ToString()}";
                        result.IsSucess = false;
                        return result;
                    }
                    else
                    {
                        result.Message = $"Successful validated input for {gameInputType.ToString()}";
                        result.Status |= GameEnum.GameStatus.ProcessingInstructions;
                        result.IsSucess = true;
                    }
                    break;

            }
        

            return result;
        }

        //public bool CheckForGameTermination(string input)
        //{
        //    bool terminateGame = false;

        //    if (!string.IsNullOrEmpty(input) && input.Trim().ToUpper() == "EXIT")
        //    {
        //        Console.WriteLine();
        //        Console.WriteLine("Exiting the game.......Thank you for playing!");
        //        Console.WriteLine();

        //        return terminateGame = true;
        //    }

        //    return terminateGame;

        //}

        private bool IsValidRegex(string input, string pattern)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            try
            {
                Regex.Match(input, pattern);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

    }
}
