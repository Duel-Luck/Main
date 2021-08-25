using System;
using System.IO;
using System.Collections.Generic;

namespace Jordan_Rogers_Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            string pName;

                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("");

                Console.WriteLine("Please Enter Your Name :");
                pName = Console.ReadLine();
                /*checking valid name*/
                bool flag = false;
                while (flag == false)
                {
                    if (pName == "" || pName == null)
                    {
                        Console.WriteLine("Incorrect name. Please enter the correct name :");
                        pName = Console.ReadLine();
                    }
                    else
                    {
                        flag = true;
                    }
                }
                Console.WriteLine($"Welcome {pName} to Battleship");

                bool playing = true;
                while (playing)
                {

                    /* Game logic*/
                    int hit = 0;
                    int Score = 0;
                    string levelname = "Impossible";
                    int cannonballs = 10;
                    int enemyships = 5;
                    Console.WriteLine("*******************************************************************************");
                    Console.WriteLine($"Currently playing Level {levelname}");
                    Console.WriteLine($"Holding {cannonballs} Cannonballs");
                    Console.WriteLine($"Sensors detect {enemyships} Hostile Targets");
                    Console.WriteLine($"You are currently holding {cannonballs} Cannonballs");
                    Console.WriteLine($"{pName} hunt and destroy all the enemyships");
                    Console.WriteLine("");
                    Console.WriteLine("*******************************************************************************");

                    /* create map variables */
                    int mapSize = 5;
                    bool[,] map = new bool[mapSize, mapSize];

                    Random random = new Random();

                    int counter = 0;
                    while (cannonballs > 0 && enemyships > 0)
                    {
                        int ranX = random.Next(mapSize); //Randomly generate X value
                        int ranY = random.Next(mapSize); //Randomly generate Y value

                        if (map[ranX, ranY]) //check if the position is right or not
                        {
                            continue;
                        }
                        else
                        {
                            map[ranX, ranY] = true; //fill the map with enemyships
                            counter++;
                        }


                        /*play game*/



                        /*ask user to enter the co-ordinates */
                        int guessX, guessY;


                        //ask user to enter the co-ordinates
                        Console.WriteLine("Insert the X Co-ordinates (using numbers between 0 and 4)");
                        guessX = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Insert the Y Co-ordinates (using the number between 0 and 4)");
                        guessY = Int32.Parse(Console.ReadLine());




                        // if wrong co-ordinates
                        if (guessX < 0 || guessX > 4 || guessY < 0 || guessY > 4)
                        {
                            Console.WriteLine("Co-ordinates are incorrect");
                            Console.WriteLine("**************************************************************");
                            continue;
                        }

                        //check if the entered co-ordinates are same as map
                        if (map[guessX, guessY])

                        {
                            Console.WriteLine("**************************************************************");
                            Console.WriteLine("Succesfully hit target");
                            hit++;
                            cannonballs--;
                            enemyships--;
                            Score++;
                            Console.WriteLine($"{cannonballs} cannonballs remaining");
                            Console.WriteLine($"{enemyships} enemyships remaining");
                            map[guessX, guessY] = false;
                            Console.WriteLine("**************************************************************");
                            Console.WriteLine();
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("**************************************************************");
                            Console.WriteLine("Missed target");
                            cannonballs--;
                            Score--;
                            Console.WriteLine($"{enemyships} enemyships remaining");
                            Console.WriteLine($"{cannonballs} cannonballs remaining");
                            Console.WriteLine("**************************************************************");
                            CalculateNearestShip(map, guessX, guessY);
                            Console.WriteLine("**************************************************************");
                            Console.WriteLine();
                        }
                    } /*end game*/
                    WriteToScoreBoard(hit, cannonballs, enemyships, pName);
                    /*ask user if they want to play again*/
                    Console.WriteLine("Play again: Y/N");
                    playing = (Console.ReadLine().ToUpper()).Contains("Y");

                }

        }

            //Calculate distance
            static void CalculateNearestShip(bool[,] map, int guessX, int guessY)
            {
                int nearest = 10000; //create nearest variable

                for (int x = 0; x < map.GetLength(0); x++) //loop through x
                {
                    for (int y = 0; y < map.GetLength(1); y++) //loop through y
                    {
                        //find the actual ship position
                        if (map[x, y])
                        {
                            //calculate the distance
                            double distance = Math.Sqrt(Math.Pow((x - guessX), 2) + Math.Pow((y - guessY), 2));
                            nearest = (int)Math.Min(nearest, distance);
                        }
                    }

                }
                Console.WriteLine($"Enemy detected within {nearest} spaces");
            }

            static void WriteToScoreBoard(int hit, int cannonballs, int enemyships, string pName)
            {
                //win and lose message
                if (hit == 5)
                {
                    //Console.WriteLine($"You are victorious with {cannonballs} cannonballs left");
                    Scoreboard($"{pName} Succesfully defeated all enemy ships with {cannonballs} remaining.");
                    Console.WriteLine("*******************************************************");
                }
                else
                {
                    // Console.WriteLine($"You have been deafeated with {enemyships} enemyships left");
                    Scoreboard($"{pName} has failed to destroy the remaining {enemyships} enemyships");
                    Console.WriteLine("*******************************************************");
                }
            }

            static void Scoreboard(string score)
            {
                string fileName = "scores.txt";
                //Write the score to the file
                File.AppendAllText(fileName, score + '\n');
                //Read from the file
                List<string> scoreBoard = new List<string>(File.ReadAllLines(fileName));

                scoreBoard.Reverse();
                Console.WriteLine("*************************************************");
                Console.WriteLine("Score Board");
                Console.WriteLine("*************************************************");

                //read from the file
                foreach (string items in scoreBoard)
                {
                    Console.WriteLine(items);
                }
            }

    }
}

