using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SeaBattle
{
    class Program
    {
        private static CustomRandom random = new CustomRandom();

        static void Main(string[] args)
        {
            string path = @"C:\Users\Ksenia\Documents\InputUserField.txt";
            List<Point> shipsCoords = new List<Point>();
            var userMap = ReadFromFile(path);
            int[,] map = GenerateMap(shipsCoords);

            ShowMap(map, 10, 10);
        }

        private static int[,] ReadFromFile(string path)
        {
            StreamReader reader = new StreamReader(path);
            int[,] arr = new int[10, 10];
            string[] userMap = new string[10];

            for (int i = 0; i < 10; i++)
            {
                userMap[i] = reader.ReadLine();
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int.TryParse(userMap[i][j].ToString(), out arr[i, j]);
                }
            }

            return arr;
        }

        public static int[,] GenerateMap(List<Point> points)
        {
            int maxShipLength = 4;
            int mapSize = 10;
            int[,] field = new int[mapSize, mapSize];

            for (int i = maxShipLength; i > 0; i--)
            {
                for (int j = i - 1; j < maxShipLength; j++)
                {
                    GenerateShip(field, i, mapSize, points);
                }
            }

            return field;
        }


        public static void GenerateShip(int[,] map, int shiplength, int mapSize, List<Point> points)
        {
            bool done = false;
            while (!done)
            {
                int posY = random.Next(0, mapSize - 1);
                int posX = random.Next(0, mapSize - 1);
                int direction = random.Next(0, 100);

                if(map[posY, posX] == 0 && Check(map, posY, posX, shiplength, mapSize, false))
                {
                    map[posY, posX] = shiplength;
                    if (direction > 50)
                    {
                        for (int i = 0; i < shiplength; i++)
                        {
                            if (Check(map, posY, posX + i, shiplength, mapSize, true))
                            {
                                map[posY, posX + i] = shiplength;
                                done = true;
                                //points.Add(new Point(posY, posX + i));
                            }
                            else if (Check(map, posY, posX + i - shiplength, shiplength, mapSize, true))
                            {
                                map[posY, (posX + i) - shiplength] = shiplength;
                                done = true;
                                //points.Add(new Point(posY, posX + i));
                            }
                            else
                            {
                                done = false;//GenerateShip(map, shiplength, mapSize, points);
                            }
                        }

                    }
                    else
                    {
                        for (int i = 0; i < shiplength; i++)
                        {
                            if(Check(map, posY + i, posX, shiplength, mapSize, true))
                            {
                                map[posY + i, posX] = shiplength;
                                done = true;
                                //points.Add(new Point(posY + i, posX));
                            }
                            else if (Check(map, posY + i - shiplength, posX, shiplength, mapSize, true))
                            {
                                map[posY + i - shiplength, posX] = shiplength;
                                done = true;
                                //points.Add(new Point(posY - i, posX));
                            }
                            else
                            {
                                done = false; //GenerateShip(map, shiplength, mapSize, points);
                            }
                        }
                    }
                }
            }
        }


        public static bool Check(int[,] map, int posY, int posX, int shipLength, int mapSize, bool isNextAnle)
        {
            if (posY < 0 || posY > mapSize - 1 || posX < 0 || posX > mapSize - 1) return false;
            for (int y = posY - 1; y < posY + 2; y++)
            {
                if (y < 0) continue;
                if (y > mapSize - 1) continue;
                for (int x = posX - 1; x < posX + 2; x++)
                {
                    if (x < 0 || x > mapSize - 1) continue;
                    if (y == posY && x == posX) continue;
                    if (isNextAnle)
                    {
                        if (y == posY - 1 && x == posX && map[y, x] == shipLength) continue;
                        if (y == posY + 1 && x == posX && map[y, x] == shipLength) continue;
                        if (y == posY && x == posX - 1 && map[y, x] == shipLength) continue;
                        if (y == posY && x == posX + 1 && map[y, x] == shipLength) continue;
                    }
                    if (map[y, x] != 0)
                        return false;
                }
            }

            return true;
        }

        public static void Shoot()
        {

        }

        //private static List<Point> ShootingCoordinates(int mapLength){
        //    for (int i = 0; i < mapLength; i++)
        //    {
        //        for (int j = 0; j < mapLength; j++)
        //        {

        //        }
        //    }
        //}

        public static void ShowMap(int[,] map, int high, int width)
        {
            Console.WriteLine("    A B C D E F G H I J");
            Console.WriteLine("   ---------------------");

            for (int y = 0; y < high; y++)
            {
                if (y + 1 < 10)
                    Console.Write($"{y + 1} ");
                else
                    Console.Write(y + 1);

                Console.Write("| ");
                for (int x = 0; x < width; x++)
                {
                    Console.Write($"{map[y, x]} ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   ---------------------");
        }
    }
}
