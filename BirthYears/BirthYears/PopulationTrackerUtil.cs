using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace BirthYears
{

    class PopulationTrackerUtil
    {
        public static int START_DATE = 1900;
        public static int END_DATE = 2000;
                    

        //Parses a file from the give path and creates a List of Person objects to return.
        public static List<Person> parseFile(String path)
        {
            List<Person> people = new List<Person>();
            try {
                FileStream fs = File.OpenRead(path);
                StreamReader reader = new StreamReader(fs);

                //Read each line and increment values of corresponding 'yearCount' indices
                while (!reader.EndOfStream)
                {
                    String[] values = reader.ReadLine().Split();

                    if (values.Length >= 2)
                    {
                        try
                        {
                            people.Add(new Person(Int32.Parse(values[0]), Int32.Parse(values[1])));
                        }
#pragma warning disable CS0168 // Variable is declared but never used
                        catch (FormatException e)
                        {
                            Console.WriteLine("Wrong input format, returning null...");
                            reader.Close();
                            fs.Close();
                            reader = null;
                            fs = null;
                            return null;
                        }
                    }
                }

                //Close streams
                reader.Close();
                fs.Close();
                reader = null;
                fs = null;
                return people;
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("Input file not found. Closing...");
                return null;
            }
            
        }

        //Given the list of people, returns an int array where each index represents a year between START_DATE and END_DATE.
        //The int corresponding to each index is the population of that year.
        public static int[] tallyYears(List<Person> people)
        {
            //Each index represents a year between START_DATE and END_DATE.
            //The int corresponding to each index is the number of people alive that year
            int[] yearsCount = new int[END_DATE - START_DATE + 1];

            foreach(Person p in people)
            {
                if(p.birth >= START_DATE && p.birth <= END_DATE && p.death >= p.birth && p.death <= END_DATE)
                {
                    //birth is within valid date range, tally their count
                    for (int i = p.birth - START_DATE; i <= p.death - START_DATE; i++)
                    {
                        yearsCount[i]++;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid dates for birth: {0}, death: {1}... Ignoring this entry", p.birth, p.death);
                }
            }

            return yearsCount;
        }

        //Returns the year or years (if there is a tie) with the largest population.
        public static List<int> GetBiggestYear(int[] yearCount)
        {
            int max = 0;
            List<int> biggestYears = new List<int>();

            for(int i = 0; i < yearCount.Length; i++)
            {
                if(yearCount[i] > max)
                {
                    biggestYears.Clear();
                    biggestYears.Add(i + PopulationTrackerUtil.START_DATE);
                    max = yearCount[i];
                }else if(yearCount[i] == max)
                {
                    biggestYears.Add(i + PopulationTrackerUtil.START_DATE);
                }
            }

            return biggestYears;
        }
    }
}
