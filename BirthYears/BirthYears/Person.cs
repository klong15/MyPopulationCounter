using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthYears
{
    //Simple data object that holds a persons birth year and year of death
    class Person
    {
        public Person(int birth, int death)
        {
            this.birth = birth;
            this.death = death;
        }

        private int birthYear;
        private int deathYear;

        public int birth
        {
            get
            {
                return birthYear;
            }
            set
            {
                birthYear = value;
            }
        }

        public int death
        {
            get
            {
                return deathYear;
            }
            set
            {
                deathYear = value;
            }
        }
    }
}
