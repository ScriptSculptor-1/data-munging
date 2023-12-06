using System;
using System.IO;

class FootballDataAnalyzer
{
    static void Main(string[] args)
    {
        string path = "./football.dat";
        CalculateSmallestGoalDifference(path);
    }

    static void CalculateSmallestGoalDifference(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            string teamWithSmallestDiff = "";
            int smallestDiff = int.MaxValue;

            for (int i = 1; i < lines.Length; i++) // data starts from line 2
            {
                string[] columns = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

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
}
