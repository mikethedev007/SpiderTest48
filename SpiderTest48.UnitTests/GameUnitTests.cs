using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpiderTest48.Domain.Dtos;
using SpiderTest48.Domain.Interfaces;
using SpiderTest48.MainApp;
using System;
using System.IO;
using System.Text;


namespace SpiderTest48.UnitTests
{
    [TestClass]
    public class GameUnitTests
    {

        //private StringBuilder ConsoleOutput { get; set; }

        public GameUnitTests() { }


        #region Auxillary Methods
        /// <summary>
        /// Auxillary Methods - These help to Arrange, Act and Assert the Unit Tests
        /// </summary>

        protected Position GetCurrentPosition()
        {
            Position currentPosition = new Position() { X = 2, Y = 3 , Orientation = Domain.Enums.GameEnum.Orientation.Right};
            return new Position();

        }


        #endregion


        #region Unit Tests

        //[TestInitialize]
        //public void Init()
        //{
        //    Console.SetOut(new StringWriter(this.ConsoleOutput));    // Associate StringBuilder with StdOut
        //    this.ConsoleOutput.Clear();    // Clear text from any previous text runs
        //}


        [TestMethod]
        public void ExplorationInstructions_Input_Fail_TestMethod()
        {
            //Arrange
            IGame game = new Game();
            Position dimension = new Position()
            {
                X = 4,
                Y = 4,
                Orientation = Domain.Enums.GameEnum.Orientation.None
            };


            Result result = new Result();
            Position currentPosition = new Position()
            {
                X = 2,
                Y = 3,
                Orientation = Domain.Enums.GameEnum.Orientation.Right
            };

            //This should fail as it contains the letter P which regex validation does not accept
            string instructions = "FRLFFLPP";

            //Act
            game.ResetWallGridPositions(dimension);
            game.UpdateSpiderCurrentPosition(currentPosition);
            result = game.ProcessInstructions(instructions);


            //Assert
            Assert.IsTrue(result.IsSucess);


        }


        [TestMethod]
        public void ExplorationInstructions_Input_Pass_TestMethod()
        {
            //Arrange
            IGame game = new Game();
            Position dimension = new Position()
            {
                X = 4,
                Y = 4,
                Orientation = Domain.Enums.GameEnum.Orientation.None
            };


            Result result = new Result();
            Position currentPosition = new Position()
            {
                X = 2,
                Y = 3,
                Orientation = Domain.Enums.GameEnum.Orientation.Right
            };

            //This should pass as it contains valid letters which regex validation accepts
            string instructions = "FLFRFLFRS";


            //Act
            game.ResetWallGridPositions(dimension);
            game.UpdateSpiderCurrentPosition(currentPosition);
            result = game.ProcessInstructions(instructions);


            //Assert
            Assert.IsTrue(result.IsSucess);


        }


        //[TestMethod]
        //public void Dimensions_Input_Fail_TestMethod()
        //{
        //    //Arrange
        //    IGame game = new Game();



        //    //Act
        //    game.DisplayWallGrid();


        //    //Assert
        //    Assert.IsTrue(true);


        //}


        //[TestMethod]
        //public void Dimensions_Input_Pass_TestMethod()
        //{
        //    //Arrange
        //    IGame game = new Game();



        //    //Act
        //    game.DisplayWallGrid();


        //    //Assert
        //    Assert.IsTrue(true);


        //}



        //[TestMethod]
        //public void CurrentLocOrientation_Input_Fail_TestMethod()
        //{
        //    //Arrange
        //    IGame game = new Game();



        //    //Act
        //    game.DisplayWallGrid();


        //    //Assert
        //    Assert.IsTrue(true);


        //}


        //[TestMethod]
        //public void CurrentLocOrientation_Input_Pass_TestMethod()
        //{
        //    //Arrange
        //    IGame game = new Game();



        //    //Act
        //    game.DisplayWallGrid();


        //    //Assert
        //    Assert.IsTrue(true);


        //}









        #endregion


    }
}
