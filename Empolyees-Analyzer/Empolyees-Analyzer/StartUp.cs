using System;

namespace EmpolyeesAnalyzer
{
    public class Startup
    {
        static void Main()
        {
            Console.WriteLine("Please add a file location");

            var fileLocation = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(fileLocation);


        }
    }
}
