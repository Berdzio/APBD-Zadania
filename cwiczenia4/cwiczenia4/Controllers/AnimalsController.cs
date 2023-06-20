using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cwiczenia4.Entites;
using cwiczenia4.Services;

namespace cwiczenia4.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalDbService _AnimalDbService;

        public AnimalsController(IAnimalDbService _AnimalDbService)
        {
            this._AnimalDbService = _AnimalDbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals([FromQuery] string orderBy)
        {
            List<Animal> animals = null;
            animals = _AnimalDbService.GetAnimals(orderBy); 
           
            return Ok(animals);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimal([FromBody] Animal animal)
        {
           _AnimalDbService.CreateAnimal(animal); 
           
            return Ok("Succsesfully created!");
        }

        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> ChangeAnimal([FromRoute] int idAnimal, [FromBody] Animal animal)
        {
            _AnimalDbService.ChangeAnimal(idAnimal, animal);
            
            return Ok("Succsesfully changed!");
        }

        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int idAnimal)
        {
             _AnimalDbService.DeleteAnimal(idAnimal);
            
            return Ok("Succsesfully deleted!");
        }


    }
}