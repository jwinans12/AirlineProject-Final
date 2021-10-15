using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlinemaker.Data
{
     public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Job { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }


        //Have Database auto-increment this maybe
        public int FlightNumber { get; set; }

        public Passenger()
        {

        }

        public Passenger(string name, string job, string email, int age, int flightNumber)
        {
            this.Name = name;
            this.Job = job;
            this.Email = email;
            this.Age = age;
            this.FlightNumber = flightNumber;
        }
    }
}
