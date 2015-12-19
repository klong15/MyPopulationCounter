using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BirthYears
{
    //This class uses static methods to generate a data file used with PopulationTrackerUtil
    class DataGenerator
    {
        private static string DATA_PATH = "datalist.txt"; 

        //Creates a new data file and returns the path to the file.
        //The total parameter specifies how many people to generate on the list
        public static string GenerateDataFile(int total)
        {
            int birth, death;
            StreamWriter writer = new StreamWriter(DATA_PATH);
            Random rand = new Random();

            for (int i = 0; i < total; i++)
            {
                //Max value is exclusive, so must add 1
                birth = rand.Next(PopulationTrackerUtil.START_DATE, PopulationTrackerUtil.END_DATE + 1);
                death = rand.Next(birth, PopulationTrackerUtil.END_DATE + 1);

                writer.WriteLine("{0} {1}", birth, death);
            }
            writer.Close();

            return DATA_PATH;
        }
    }
}
