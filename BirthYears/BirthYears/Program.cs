using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.IO;

namespace BirthYears
{
    class Program
    {
        public static int START_DATE = 1900;
        public static int END_DATE = 2000;      

        static void Main(string[] args)
        {
            //Check for a valid command line argument, if no argument, create data file to use.
            string path;
            if(args.Length < 1)
            {
                //No file is given, so create new one to use
                path = DataGenerator.GenerateDataFile(100000);
            }
            else
            {
                path = args[0];
            }

            if(args.Length > 1)
            {
                Console.WriteLine("Invalid parameters. Please provide a path");
                return;
            }
            
            
            List<Person> people = PopulationTrackerUtil.parseFile(path);
            if (people == null) return;

            int[] yearCount = PopulationTrackerUtil.tallyYears(people);

            printResult(people, yearCount);

            FileStream outstream = File.OpenWrite("test.jpeg");
            GeneratePlot(yearCount, outstream);
            outstream.Close();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void printResult(List<Person> people, int[] yearCount)
        {
            List<int> biggestYears = PopulationTrackerUtil.GetBiggestYear(yearCount);
            Console.WriteLine("The years with the biggest populations are: ");
            foreach(int year in biggestYears)
            {
                Console.WriteLine(year+ "");
            }
            Console.WriteLine("with a population of {0}", yearCount[biggestYears[0] - PopulationTrackerUtil.START_DATE]);
        }

        /*
        Creates a graph to visualize the population between PopulationTrackerUtil.START_DATE nad PopulationTrackerUtil.END_DATE
        */
        public static void GeneratePlot(int[] yearCount, Stream outputStream)
        {
            using (var ch = new Chart())
            {
                ch.Size = new Size(1000, 1000); //Resolution of the chart
                ch.ChartAreas.Add(new ChartArea());
                var s = new Series();
                for(int i = 0; i < yearCount.Length; i++)
                {
                    int pop = yearCount[i];
                    s.Points.Add(new DataPoint(PopulationTrackerUtil.START_DATE + i, pop));
                }
                ch.Series.Add(s);
                ch.SaveImage(outputStream, ChartImageFormat.Jpeg);
            }
        }
    }

    public struct CoOrds
    {
        public int x, y;

        public CoOrds(int p1, int p2)
        {
            x = p1;
            y = p2;
        }
    }
}
