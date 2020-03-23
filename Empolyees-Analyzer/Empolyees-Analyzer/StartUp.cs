using System;

namespace EmpolyeesAnalyzer
{
    public class Startup
    {
        static void Main()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\dkats\Desktop\myText.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
    }
}
