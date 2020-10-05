using System.Reflection.Metadata.Ecma335;
using BLL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pulsaciones.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Pulsaciones.Controllers
{
    [Route("api/[controller]")]  // api/Persona
    [ApiController]
    public class PersonController: ControllerBase
    {
        private readonly PersonService personService;
        public IConfiguration Configuration {get;} 
        public PersonController(IConfiguration configuration)
        {
            Configuration = configuration;
            string connectionString = Configuration["ConnectionString:DefaultConnection"];
            personService = new PersonService(connectionString);
        }

        // POST: api/Person
        [HttpPost]
        public ActionResult<PersonViewModel> Save(PersonInputModel personInput)
        {   
            Person person = MapPerson(personInput);
            var response = personService.Save(person);

            if(response.Error) return BadRequest(response.Message);
            return Ok(response.Person);

        }

        private Person MapPerson(PersonInputModel personInput)
        {
            var person = new Person
            {
                Identification = personInput.Identification,
                Name = personInput.Name,
                Sex = personInput.Sex,
                Age =  personInput.Age
            };
            return person;
        }
        // GET: api/Person
        [HttpGet]
        public ActionResult<IEnumerable<PersonViewModel>> GetList()
        {
            ConsultPersonResponse response = personService.GetList();

            if(response.Error) BadRequest(response.Message);
            var personas  = response.Persons.Select(p => new PersonViewModel(p));

            return Ok(personas);
        }

        [HttpGet("{identification}")]
        public ActionResult<PersonViewModel> SearchById(string identification)
        {
            SearchPersonResponse response =  personService.SearchById(identification);

            if(response.Person == null) return NotFound("Persona no encontrada!");
            var personViewModel = new PersonViewModel(response.Person);
            return Ok(personViewModel);
        }

        [HttpDelete("{identification}")]
        public ActionResult<string> Delete(string identification)
        {
            return Ok(personService.Delete(identification));
        }
        
        [HttpPut]
        public ActionResult<string> Modify(PersonInputModel personInput)
        {
            Person person = MapPerson(personInput);
            return Ok(personService.Modidy(person));
        }
    }
}