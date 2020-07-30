using GreenVsRedGame;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstRealWork
{
    public class Program
    {
        const int redValue = 0;
        const int greenValue = 1;
        static void Main(string[] args)
        {
            // If point with red value "meet" n-numbers green neighbours change its value to green
            List<int> redChangesValues = new List<int>() { 3, 6 };
            // If point with green value "meet" n-numbers red neighbours change its value to red
            List<int> greenChangesValues = new List<int>() { 0, 1, 4, 5, 7, 8 };

            // Input information in format x, y; x - width = cows and y - hight = rows
            int[] coordinates = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int cows = coordinates[0];
            int rows = coordinates[1];

            //Generate grid with relevant size and fullFill the initial statement of grid - GenerationZero
            Point[,] grid = FullFillTheGridWIthInputInformation(rows, cows);

            // Input information in format x1, y1, number of "generation"
            int[] additionalInfoInput = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int x = additionalInfoInput[0];
            int y = additionalInfoInput[1];
            int generationCount = additionalInfoInput[2];

            // We are looking for: how many time point with coordinates x for cow and y for row will be green-with value 
            int counterOfChangesOfGivenPoint = 0;

            Point[,] futureGrid = new Point[rows, cows];

            while (generationCount != 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cows; j++)
                    {
                        // This is the point to chech how many time is green
                        if (i == y && j == x && grid[i, j].Value == 1)
                        {
                            counterOfChangesOfGivenPoint++;
                        }

                        int countOfGreenNeighbors = HowManyGreenNeighbors(i, j, grid);
                        int currStatmenOfPoint = grid[i, j].Value;
                        if (currStatmenOfPoint == redValue && redChangesValues.Contains(countOfGreenNeighbors)) // it's red 0
                        {
                            futureGrid[i, j].Value = greenValue;
                        }
                        else if (currStatmenOfPoint == greenValue && greenChangesValues.Contains(countOfGreenNeighbors)) // it's green 1
                        {
                            futureGrid[i, j].Value = redValue;
                        }
                    }
                }

                // Change/ Replace the old with the new generation - take information from futureGrid
                grid = ChangeTheCurrentGenerationWithNewValues(futureGrid);
                generationCount--;
            } // end while


            if (futureGrid[y, x].Value == greenValue)
            {
                counterOfChangesOfGivenPoint++;
            }

            Console.WriteLine(counterOfChangesOfGivenPoint);

        }

        public static Point[,] FullFillTheGridWIthInputInformation(int rows, int cows)
        {
            Point[,] grid = new Point[rows, cows];
            for (int i = 0; i < rows; i++)
            {
                string inputInformation = Console.ReadLine();
                for (int j = 0; j < cows; j++)
                {
                    int currNumber = int.Parse(inputInformation[j].ToString());
                    grid[i, j] = new Point(currNumber, i, j);
                }
            }
            return grid;
        }

        public static int HowManyGreenNeighbors(int currRow, int currCow, Point[,] currentGrid)
        {
            //.GetLength(0) for the number of rows
            int maxRow = currentGrid.GetLength(0);
            //.GetLength(1) for the number of columns
            int maxCow = currentGrid.GetLength(1);

            int countOfGreenNeighbors = 0;

            //We have to check every possiible "Neighbor" if is in border of the matrix-grid
            // If coordinates are valid, we have check if it's with green color - with vaue 1
            // UpLeft
            bool isValid = IsValidCoordinatesOfMyNeighbor((currRow - 1), (currCow - 1), maxRow, maxCow);
            if (isValid)
            {
                if (currentGrid[(currRow - 1), (currCow - 1)].Value == greenValue)
                {
                    countOfGreenNeighbors++;
                }
            }

            //Up
            isValid = IsValidCoordinatesOfMyNeighbor((currRow - 1), (currCow), maxRow, maxCow);
            if (isValid)
            {
                if (currentGrid[(currRow - 1), (currCow)].Value == greenValue)
                {
                    countOfGreenNeighbors++;
                }
            }

            //UpRight
            isValid = IsValidCoordinatesOfMyNeighbor((currRow - 1), (currCow + 1), maxRow, maxCow);
            if (isValid)
            {
                if (currentGrid[(currRow - 1), (currCow + 1)].Value == greenValue)
                {
                    countOfGreenNeighbors++;
                }
            }

            //Left
            isValid = IsValidCoordinatesOfMyNeighbor((currRow), (currCow - 1), maxRow, maxCow);
            if (isValid)
            {
                if (currentGrid[(currRow), (currCow - 1)].Value == greenValue)
                {
                    countOfGreenNeighbors++;
                }
            }

            //Right
            isValid = IsValidCoordinatesOfMyNeighbor((currRow), (currCow + 1), maxRow, maxCow);
            if (isValid)
            {
                if (currentGrid[(currRow), (currCow + 1)].Value == greenValue)
                {
                    countOfGreenNeighbors++;
                }
            }

            //DownLeft
            isValid = IsValidCoordinatesOfMyNeighbor((currRow + 1), (currCow - 1), maxRow, maxCow);
            if (isValid)
            {
                if (currentGrid[(currRow + 1), (currCow - 1)].Value == greenValue)
                {
                    countOfGreenNeighbors++;
                }
            }

            //Down
            isValid = IsValidCoordinatesOfMyNeighbor((currRow + 1), (currCow), maxRow, maxCow);
            if (isValid)
            {
                if (currentGrid[(currRow + 1), (currCow)].Value == greenValue)
                {
                    countOfGreenNeighbors++;
                }
            }

            //DownRight
            isValid = IsValidCoordinatesOfMyNeighbor((currRow + 1), (currCow + 1), maxRow, maxCow);
            if (isValid)
            {
                if (currentGrid[(currRow + 1), (currCow + 1)].Value == greenValue)
                {
                    countOfGreenNeighbors++;
                }
            }

            return countOfGreenNeighbors;
        }

        public static bool IsValidCoordinatesOfMyNeighbor(int row, int cow, int maxRow, int maxCow)
        {
            return row >= 0 && row < maxRow && cow >= 0 && cow < maxCow;
        }

        public static Point[,] ChangeTheCurrentGenerationWithNewValues(Point[,] futureGrid)
        {
            int gridRows = futureGrid.GetLength(0);
            int gridCows = futureGrid.GetLength(1);
            Point[,] grid = new Point[gridRows, gridCows];
            for (int i = 0; i < futureGrid.GetLength(0); i++)
            {
                for (int j = 0; j < futureGrid.GetLength(1); j++)
                {
                    grid[i, j] = futureGrid[i, j];
                }
            }
            return grid;
        }

    }
}
