using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airlinemaker.Data;
using Microsoft.AspNetCore.Mvc;

namespace AirlineProject.Web.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightDAO flightDAO;
        //private readonly IPassengerDAO passengerDAO;

        public FlightsController(IFlightDAO flightDao)
        {
            this.flightDAO = flightDao;
        }

        public IActionResult Index()
        {
            IEnumerable<Flight> model = flightDAO.GetFlights();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            Flight model = flightDAO.GetFlight(id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Flight flight)
        {
            if (ModelState.IsValid)
            {
                flightDAO.AddFlight(flight);

                return RedirectToAction("Index");
            }
            return View(flight);
        }


        public IActionResult CurrentPassengers(int id)
        {
            int flightId = id;
            TempData["mydata"] = flightId;

            return RedirectToAction("GetCertainPassengers", "Passengers");
        }

        // GET: FlightsController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Flight model = flightDAO.GetFlight(id);

            return View(model);
        }

        // POST: FlightsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind] Flight flight)
        {
            try
            {
                flightDAO.DeleteFlight(flight);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(flight);
            }
        }

        // GET: FlightsController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Flight model = flightDAO.GetFlight(id);
            return View(model);
        }

        // POST: PassengersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Flight flight)
        {
            if (ModelState.IsValid)
            {
                flightDAO.EditFlight(flight);

                return RedirectToAction("Index");
            }
            return View(flight);
        }

    }
}
