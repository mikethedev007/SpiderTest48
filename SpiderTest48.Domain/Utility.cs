using SpiderTest48.Domain.Dtos;
using SpiderTest48.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain
{
    public static class Utility
    {
        public static Position ExtractPosition(this string input) 
        { 
            Position extractedPosition = new Position();
            string positionOrientation = "None";

            string[] locationOrientationValues = input.Split(' ');

            extractedPosition.X = int.Parse(locationOrientationValues[0].ToString());
            extractedPosition.Y = int.Parse(locationOrientationValues[1].ToString());

            if(locationOrientationValues.Length > 2)
            {
                positionOrientation = locationOrientationValues[2].ToString();
            }

            switch (positionOrientation.ToUpper())
            {
                case "LEFT":
                    extractedPosition.Orientation = GameEnum.Orientation.Left;
                    break;
                case "RIGHT":
                    extractedPosition.Orientation = GameEnum.Orientation.Right;
                    break;
                case "UP":
                    extractedPosition.Orientation = GameEnum.Orientation.Up;
                    break;
                case "DOWN":
                    extractedPosition.Orientation = GameEnum.Orientation.Down;
                    break;
                default:
                    extractedPosition.Orientation = GameEnum.Orientation.None;
                    break;
            }

            return extractedPosition;
        }


    }
}
