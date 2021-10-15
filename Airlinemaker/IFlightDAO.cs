using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlinemaker.Data
{
    public interface IFlightDAO
    {
        //CRUD = Create, Retrieve, Update, Delete

        public IEnumerable<Flight> GetFlights();

        public Flight GetFlight(int id);

        public void AddFlight(Flight flight);

        public void DeleteFlight(Flight flight);

        public void EditFlight(Flight flight);
    }
}
