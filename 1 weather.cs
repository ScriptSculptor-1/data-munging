using System;
using System.IO;

class WeatherDataAnalyzer
{
    static void Main(string[] args)
    {
        string path = "./weather.dat";
        CalculateSmallestTemperatureSpread(path);
    }

    static void CalculateSmallestTemperatureSpread(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            int minSpreadDay = 0;
            float minSpread = float.MaxValue;

            for (int i = 2; i < lines.Length - 1; i++) // data starts from line 3
            {
                string[] columns = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

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
}
