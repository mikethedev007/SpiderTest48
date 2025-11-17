using NAudio.Wave;
using SpiderTest48.Domain.Dtos;
using SpiderTest48.Domain.Enums;
using SpiderTest48.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpiderTest48.MainApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string wallDimensionStr = string.Empty;

            try
            {

                string audioFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\LCh.wav";
                PlayAudio(audioFilePath);

                //Set Wall Maximum Sizes allowed
                Position maxWallDimension = new Position(20, 20, GameEnum.Orientation.None);
                Wall wallDto = new Wall(maxWallDimension); 

                Game game = new Game(wallDto);

                //Run the Game until user enters EXIT
                game.InitializeGame();
                game.Run();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


        }


        protected static async Task PlayAudio(string filePath)
        {

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(fs);
            sp.PlayLooping();
        }


    }
}
