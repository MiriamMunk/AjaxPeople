using DbManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication26.Models;

namespace WebApplication26.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";
        public IActionResult People()
        {
            return View();
        }
        public IActionResult GetAllPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            List<Person> people = repo.GetPeople();
            return Json(people);
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.AddPerson(person);
        }
        [HttpPost]
        public void EditPerson(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.EditPerson(person);
        }

        [HttpPost]
        public void DeletePerson(int id)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeletePerson(id);
        }
    }
}
