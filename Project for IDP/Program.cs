using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_for_IDP
{ 
    class Program
    {
        static void Main(string[] args)
        {
            int[] xPosition = new int[50];
                xPosition[0]= 30;
            int[] yPosition = new int[50];
                yPosition[0] = 30;
            int appleXPosition = 10;
            int appleYPosition = 10;
            int applesEaten = 0;

            decimal gameSpeed = 150m;

            bool gameOn = true;
            bool hitWall = false;
            bool appleEaten = false;

            Random random = new Random();

            Console.CursorVisible = false;

            // Paint the snake
            paintSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

            // Spawn the first apple
            spawnApple(random, out appleXPosition, out appleYPosition);
            paintApple(appleXPosition, appleYPosition);
            
            //Build the boarder
            buildAWall();

            ConsoleKey command = Console.ReadKey().Key;

            //Controls for the snake
            do
            {
                switch (command)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]--;
                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]--;
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]++;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]++;
                        break;
                }

                //Countinue painting the snake
                paintSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

                //Check if the snake collides with a wall
                hitWall = isWallHit(xPosition[0], yPosition[0]);

                if (hitWall)
                {
                    gameOn = false;
                    Console.SetCursorPosition(28, 20);
                    Console.WriteLine("Game Over. Blub.");
                }

                //Set the speed of the snake
                if (Console.KeyAvailable) command = Console.ReadKey().Key;
                System.Threading.Thread.Sleep(Convert.ToInt32(gameSpeed));

                //Check how many apples are 
                appleEaten = checkIsAppleEaten(xPosition[0], yPosition[0], appleXPosition, appleYPosition);

                if (appleEaten)
                {
                    spawnApple(random, out appleXPosition, out appleYPosition);
                    paintApple(appleXPosition, appleYPosition);
                    applesEaten++;
                    //Increase game speed when an apple is eaten
                    gameSpeed *= .925m;
                }
            } while (gameOn);
        }

        private static void paintSnake(int applesEaten, int[] xPositionIn, int[] yPositionIn, out int[] xPositionOut, out int[] yPositionOut)
        {
            xPositionOut = xPositionIn;
            yPositionOut = yPositionIn;

            //Paint head of the snake
            Console.SetCursorPosition(xPositionIn[0], yPositionIn[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine('S');

            for (int i = 1; i < applesEaten + 1; i++)
            {
                //Paint body of the snake
                Console.SetCursorPosition(xPositionIn[i], yPositionIn[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("o");
            }

            Console.SetCursorPosition(xPositionIn[applesEaten + 1], yPositionIn[applesEaten + 1]);
            Console.WriteLine(" ");

            for (int i = applesEaten + 1; i > 0; i--)
            {
                xPositionIn[i] = xPositionIn[i - 1];
                yPositionIn[i] = yPositionIn[i - 1];
            }

        }

        private static void paintApple(int appleXPosition, int appleYPosition)
        {
            //Spawn apples
            Console.SetCursorPosition(appleXPosition, appleYPosition);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('a');
        }

        private static void spawnApple(Random random, out int appleXPosition, out int appleYPosition)
        {
            //Spawn the apples inside the boarder
            appleXPosition = random.Next(0 + 2, 70 - 2);
            appleYPosition = random.Next(0 + 2, 40 - 2);
        }

        private static bool isWallHit(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == 70 || yPosition == 1 || yPosition == 40)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void buildAWall()
        {
            for (int i = 1; i < 41; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write('#');
                Console.SetCursorPosition(70, i);
                Console.Write('#');
            }

            for (int i = 1; i < 71; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i,1);
                Console.Write('#');
                Console.SetCursorPosition(i,40);
                Console.Write('#');
            }
        }

        private static bool checkIsAppleEaten(int xPosition, int yPosition, int appleXPosition, int appleYPosition)
        {
            if (xPosition == appleXPosition && yPosition == appleYPosition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
