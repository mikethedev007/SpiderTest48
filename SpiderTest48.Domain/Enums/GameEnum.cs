using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain.Enums
{
    public class GameEnum
    {

        public enum GameInputType
        {
            None = 0,
            [Display(Name="Input values for Dimensions")]
            Dimensions = 1,
            [Display(Name = "Input values for Current Location Orientation")]
            CurrentLocationOrientation = 2,
            [Display(Name = "Input values for Instructions For Exploration")]
            InstructionsForExploration = 3
        }

        public enum Orientation
        {
            [Display(Name="No orientation")]
            None = 0,
            [Display(Name = "Left orientation")]
            Left = 1,
            [Display(Name = "Right orientation")]
            Right = 2,
            [Display(Name = "Up orientation")]
            Up = 3,
            [Display(Name = "Down orientation")]
            Down = 4
        }

        public enum GridSpace
        {
            Empty = 0,
            Occupied = 1
        }

        public enum GameStatus
        {
            None = 0,
            ProcessingDimensions = 1,
            ProcessingCurrentLocOrientation = 2,
            ProcessingInstructions = 3,
            ExitGame = 4

        }
    }
}
