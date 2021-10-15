using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Airlinemaker.Data;

namespace AirlineProject.Web.Controllers
{
    public class PassengersController : Controller
    {
        private readonly IPassengerDAO passengerDAO;

        public PassengersController (IPassengerDAO passengerDao) {
            this.passengerDAO = passengerDao;
        }



        // GET: PassengersController
        public IActionResult Index()
        {
            IEnumerable<Passenger> model = passengerDAO.GetPassengers();

            return View(model);
        }

        // GET: PassengersController/Details/5
        public IActionResult Details(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);

            return View(model);
        }

        // GET: PassengersController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PassengersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                passengerDAO.AddPassenger(passenger);

                return RedirectToAction("Index");
            }
            return View(passenger);
        }

        // GET: PassengersController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);
            return View(model);
        }

        // POST: PassengersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                passengerDAO.EditPassenger(passenger);

                return RedirectToAction("Index");
            }
            return View(passenger);
        }

        // GET: PassengersController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);

            return View(model);
        }

        // POST: PassengersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind] Passenger passenger)
        {
            try
            {
                passengerDAO.DeletePassenger(passenger);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(passenger);
            }
        }

        public IActionResult GetCertainPassengers()
        {
            int flightId = (int)TempData["mydata"];
            IEnumerable<Passenger> model = passengerDAO.GetPassengersByFlight(flightId);

            return View(model);
        }
    }
}
