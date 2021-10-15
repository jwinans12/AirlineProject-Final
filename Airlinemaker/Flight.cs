using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlinemaker.Data
{
    public class Flight
    {
        public int Id { get; set; }

        [Display(Name = "Departing From:")]
        public string DepartLoc { get; set; }

        [Display(Name = "Arrival Location:")]
        public string ArriveLoc { get; set; }

        [Display(Name = "Departure Date:")]
        [DataType(DataType.Date)]
        public string DepartDate { get; set; }

        [Display(Name = "Arrival Date:")]
        [DataType(DataType.Date)]
        public string ArriveDate { get; set; }

        [Display(Name = "Departure Time:")]
        public string DepartTime { get; set; }

        [Display(Name = "Arrival Time:")]
        public string ArriveTime { get; set; }

        [Display(Name = "Maximum Capacity:")]
        public int PassengerLimit { get; set; }


        public Flight() { }

        public Flight(string departLoc, string arriveLoc, string departDate, string arriveDate, string departTime, string arriveTime, int passengerLimit)
        {
            this.DepartLoc = departLoc;
            this.DepartDate = departDate;
            this.DepartTime = departTime;
            this.ArriveLoc = arriveLoc;
            this.ArriveDate = arriveDate;
            this.ArriveTime = arriveTime;
            this.PassengerLimit = passengerLimit;
        }

    }
}
