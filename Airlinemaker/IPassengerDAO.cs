using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlinemaker.Data
{
    public interface IPassengerDAO
    {
        public IEnumerable<Passenger> GetPassengers();

        public Passenger GetPassenger(int id);

        public void AddPassenger(Passenger passenger);
        public void DeletePassenger(Passenger passenger);
        public void EditPassenger(Passenger passenger);

        public IEnumerable<Passenger> GetPassengersByFlight(int flightNumber);
    }
}
