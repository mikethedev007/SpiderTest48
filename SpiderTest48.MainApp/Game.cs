using SpiderTest48.Domain;
using SpiderTest48.Domain.Dtos;
using SpiderTest48.Domain.Enums;
using SpiderTest48.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static SpiderTest48.Domain.Enums.GameEnum;

namespace SpiderTest48.MainApp
{
    //Structure similar to Facade Pattern to manage Game operations
    public class Game  : IGame
    {
        public Wall WallDto { get; set; }
        public List<GameQuestion> Questions = new List<GameQuestion>();

        private Position SpiderCurrentPosition {  get; set; }

        public Game()
        {
            WallDto = new Wall();
            WallDto.Dimension = new Position(10, 10, GameEnum.Orientation.None);
        }

        public Game(Wall wallDto)
        {
            WallDto = wallDto;
        }

        public void InitializeGame()
        {
            StringBuilder sb = new StringBuilder();

            //Initialise Questions
            Questions.Add(new GameQuestion() 
            { 
                Id = 1, 
                Question = "Please the size of the wall. 0 0 (bottom left) to x y\r\n(Top right). Enter 'EXIT' to quit the game.", 
                QuestionType = GameEnum.GameInputType.Dimensions, 
                IsAnswered = false
            });

            sb.Append("Please enter the spider’s current location and orientation.\r\n");
            sb.Append("This is made up of two integers and a word separated by spaces, corresponding to the x and \r\n");
            sb.AppendLine("y coordinates and the spider's orientation. E.g. 4 10 Left\r\n");

            Questions.Add(new GameQuestion()
            {
                Id = 2,
                Question = sb.ToString(),
                QuestionType = GameEnum.GameInputType.CurrentLocationOrientation,
                IsAnswered = false
            });

            sb.Clear();

            sb.Append("Please enter the series of instructions telling the spider how to explore the\r\n");
            sb.AppendLine("wall. E.g. FLFLFRFFLF");

            Questions.Add(new GameQuestion()
            {
                Id = 3,
                Question = sb.ToString(),
                QuestionType = GameEnum.GameInputType.InstructionsForExploration,
                IsAnswered = false
            });

        }

        public void Run()
        {
            bool exitGame = false;
            SpiderCurrentPosition = new Position();

            Console.WriteLine($"Window Width: {Console.WindowWidth}, Window Height: {Console.WindowHeight}");
            DisplayHeading();

            //Accept input until user wants to exit
            while (!exitGame)
            {
                Result result = new Result();
                Result instructionsResult = new Result();

                //Loop through each question
                foreach (GameQuestion question in Questions) 
                {
                    //Process the question
                    result = ProcessQuestion(question);

                    //check if the user has entered to exit game
                    if (result != null)
                    {
                        if(result.Status == GameEnum.GameStatus.ExitGame)
                        {
                            exitGame = true;
                            break;
                        }

                        //if last question is successful then update wall grid with instructions
                        if(question.QuestionType == GameInputType.InstructionsForExploration 
                            && result.IsSucess)
                        {
                            exitGame = true;
                            break;
                        }

                    }

                }

            }

            if(exitGame)
            {
                Console.WriteLine("Please ANY key to EXIT.....");
            }
            else
            {
                Console.WriteLine("Please ANY key to go back to main menu.....");

            }
            Console.ReadKey();
            Console.Clear();

        }

        private void DisplayHeading()
        {
            Console.WriteLine();

            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write("=");

            //Let the user know the maximum allowed size of wall 
            Console.WriteLine();
            Console.WriteLine(
                $"The Maximum dimensions of the wall which can be entered are : X = {WallDto.Dimension.X} and Y = {WallDto.Dimension.Y}");
            Console.WriteLine();

            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write("=");

            Console.WriteLine();
            Console.WriteLine();

        }

        public void ResetWallGridPositions(Position dimension)
        {
            WallDto.Dimension = dimension;
            WallDto.InitializeWallGridPositions();
        }

        public Result ProcessQuestion(GameQuestion gameQuestion)
        {
            IValidation validation = new Validation();
            Result result = new Result();
            Result instructionsResult = new Result();

            string inputString = string.Empty;

            //Accept input from user until valid entry or user types EXIT to quit the Game
            while (true) 
            {
                //Display Question
                Console.WriteLine($"Question {gameQuestion.Id}: { gameQuestion.Question }");
                inputString = Console.ReadLine();

                inputString = inputString.Trim();

                //Validate user input
                result = validation.ValidateInput(inputString, gameQuestion.QuestionType);

                //Check if user has typed EXIT to quit the game
                if (result.Status == GameEnum.GameStatus.ExitGame)
                {
                    return result;
                }

                //Check if the user has provided a valid answer
                if(result.IsSucess)
                {
                    //set the Wall Grid Dimensions
                    if(gameQuestion.QuestionType == GameInputType.Dimensions)
                    { 
                        WallDto.Dimension = Utility.ExtractPosition(inputString);

                        //reset wallgrid positions
                        ResetWallGridPositions(WallDto.Dimension);
                    }

                    //set SpiderCurrentPosition when question has been answered
                    if(gameQuestion.QuestionType == GameInputType.CurrentLocationOrientation)
                    {
                        SpiderCurrentPosition = Utility.ExtractPosition(inputString);
                    }

                    //if last question is successful then update wall grid with instructions
                    if (gameQuestion.QuestionType == GameInputType.InstructionsForExploration)
                    {
                        instructionsResult = ProcessInstructions(inputString);

                    }

                    //break out of current question loop to go to next question or finish
                    break;

                }
                else 
                {
                    //user has not entered a valid entry so ask to enter a valid entry
                    Console.WriteLine("Sorry, you entered an invalid entry......");

                    //reset result if invalid input has been entered
                    result = null;
                }


            }

            //clear the screen
            Console.WriteLine();
            Console.Clear();
            Console.WriteLine();


            return result;
        }

        public Result ProcessInstructions(string input)
        {
            Result result = new Result();

            Position newPosition;
            WallPosition wallPosition;

            //split each Character in string
            char[] instructions = input.ToUpper().ToCharArray();

            foreach (char instruction in instructions) 
            {

                newPosition = new Position();
                wallPosition = new WallPosition();

                if (instruction == 'F')
                {
                    switch (SpiderCurrentPosition.Orientation)
                    {
                        case Orientation.Up:
                            newPosition.Orientation = Orientation.Up;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y + 1;
                            break;
                        case Orientation.Down:
                            newPosition.Orientation = Orientation.Down;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y - 1;
                            break;
                        case Orientation.Left:
                            newPosition.Orientation = Orientation.Left;
                            newPosition.X = SpiderCurrentPosition.X - 1;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;
                        case Orientation.Right:
                            newPosition.Orientation = Orientation.Right;
                            newPosition.X = SpiderCurrentPosition.X + 1;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;


                    }
                }
                else if(instruction == 'R')
                {

                    switch (SpiderCurrentPosition.Orientation)
                    {
                        case Orientation.Up:
                            newPosition.Orientation = Orientation.Right;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;
                        case Orientation.Down:
                            newPosition.Orientation = Orientation.Left;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;
                        case Orientation.Left:
                            newPosition.Orientation = Orientation.Up;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;
                        case Orientation.Right:
                            newPosition.Orientation = Orientation.Down;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;
                    }

                }
                else 
                {
                    //instruction = L

                    switch (SpiderCurrentPosition.Orientation)
                    {
                        case Orientation.Up:
                            newPosition.Orientation = Orientation.Left;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;
                        case Orientation.Down:
                            newPosition.Orientation = Orientation.Right;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;
                        case Orientation.Left:
                            newPosition.Orientation = Orientation.Down;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;
                        case Orientation.Right:
                            newPosition.Orientation = Orientation.Up;
                            newPosition.X = SpiderCurrentPosition.X;
                            newPosition.Y = SpiderCurrentPosition.Y;
                            break;


                    }
                }

                //Update WallGrid
                wallPosition.Position = newPosition;
                SpiderCurrentPosition = newPosition;
                UpdateWallGrid(wallPosition);


            }

            result.Status = GameStatus.ProcessingInstructions;
            result.Message = "WallGrid has been updated.";

            //Output is
            Console.WriteLine($"The Output is {SpiderCurrentPosition.X.ToString()} " +
                $"{SpiderCurrentPosition.Y.ToString()} " +
                $"{SpiderCurrentPosition.Orientation.ToString()}.");

            Console.WriteLine("Press any key.....");
            Console.ReadKey();

            Console.WriteLine("Displaying the final grid");
            DisplayWallGrid();

            Console.WriteLine("Press any key.....");
            Console.ReadKey();

            result.IsSucess = true;

            return result;
        }


        public void UpdateWallGrid(WallPosition newWallPosition)
        {
            //print moves
            Console.WriteLine($"Spider position X={newWallPosition.Position.X}, " +
                $" Y={newWallPosition.Position.Y}, " +
                $" Orientation={newWallPosition.Position.Orientation.ToString()}");

            ////reset occupied position
            if (WallDto.WallGridPositions.Any(wp => wp.IsOccupied == true))
            {
                WallDto.WallGridPositions
                    .FirstOrDefault(wp => wp.IsOccupied)
                    .IsOccupied = false;
            }


            ////set new occupied position
            if (WallDto.WallGridPositions.Any(wp => wp.Position.X == newWallPosition.Position.X
                           && wp.Position.Y == newWallPosition.Position.Y))
            {
                WallDto.WallGridPositions
                       .FirstOrDefault(wp => wp.Position.X == newWallPosition.Position.X
                           && wp.Position.Y == newWallPosition.Position.Y)
                       .IsOccupied = true;

            }

        }

        public void UpdateSpiderCurrentPosition(Position position)
        {
            SpiderCurrentPosition = position;
        }

        public void DisplayWallGrid()
        {

            WallPosition newWallPosition = new WallPosition();
            string displayChar = ".";
            bool displaySpider = false;

           int gap = 5;
            for (int y = 0; y < WallDto.Dimension.Y; y++)
            {
                for (int x = 0; x < WallDto.Dimension.X; x++)
                {
                    if (SpiderCurrentPosition.X == x && SpiderCurrentPosition.Y == y)
                    {
                        switch (SpiderCurrentPosition.Orientation)
                        {
                            case GameEnum.Orientation.Up:
                                displayChar = "^@";
                                break;
                            case GameEnum.Orientation.Down:
                                displayChar = "@v";
                                break;
                            case GameEnum.Orientation.Left:
                                displayChar = "<@";
                                break;
                            case GameEnum.Orientation.Right:
                                displayChar = "@>";
                                break;
                        }


                    }

                    if(x != WallDto.Dimension.X - 1) 
                    {
                        Console.Write(displayChar.PadRight(gap));
                    }
                    else 
                    {
                        Console.WriteLine(displayChar.PadRight(gap));
                    }

                    displayChar = ".";  
                }


            }

        }


    }
}
