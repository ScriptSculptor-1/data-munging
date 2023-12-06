using System;
using System.IO;

class DataAnalyzer
{
    static void Main(string[] args)
    {
        CalculateSmallestTemperatureSpread("./weather.dat");
        CalculateSmallestGoalDifference("./football.dat");
    }

    static void CalculateSmallestTemperatureSpread(string filePath)
    {
        try
        {
            string[] lines = ReadFileLines(filePath);
            int minSpreadDay = 0;
            float minSpread = float.MaxValue;

            for (int i = 2; i < lines.Length - 1; i++) // data starts from line 3
            {
                string[] columns = SplitLine(lines[i], new char[] { ' ' });

                // making sure that the data in Day column is integer
                if (columns.Length >= 3 && int.TryParse(columns[0], out int day))
                {
                    float maxTemp = float.Parse(columns[1].Replace("*", "")); // cleaning the data
                    float minTemp = float.Parse(columns[2].Replace("*", ""));
                    float spread = maxTemp - minTemp;

                    if (spread < minSpread)
                    {
                        minSpread = spread;
                        minSpreadDay = day;
                    }
                }
            }

            Console.WriteLine($"Day with smallest temperature spread: {minSpreadDay}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

    static void CalculateSmallestGoalDifference(string filePath)
    {
        try
        {
            string[] lines = ReadFileLines(filePath);
            string teamWithSmallestDiff = "";
            int smallestDiff = int.MaxValue;

            for (int i = 1; i < lines.Length; i++) // data starts from line 2
            {
                string[] columns = SplitLine(lines[i], new char[] { ' ', '\t' });

                // making sure that the provided data for column F and A is integer type
                if (columns.Length > 8 && int.TryParse(columns[6], out int goalsFor) && int.TryParse(columns[8], out int goalsAgainst))
                {
                    int diff = Math.Abs(goalsFor - goalsAgainst);
                    if (diff < smallestDiff)
                    {
                        smallestDiff = diff;
                        teamWithSmallestDiff = columns[1];
                    }
                }
            }

            Console.WriteLine($"Team with the smallest goal difference: {teamWithSmallestDiff}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

    static string[] ReadFileLines(string filePath)
    {
        try
        {
            return File.ReadAllLines(filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            return null; // Return null to indicate failure
        }
    }

    static string[] SplitLine(string line, char[] delimiters)
    {
        return line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
    }
}
