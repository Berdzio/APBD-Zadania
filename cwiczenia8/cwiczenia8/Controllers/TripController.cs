using cwiczenia8.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cwiczenia8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ApbdContext _ApbdContext;


        public TripController(ApbdContext context)
        {
            _ApbdContext = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {



            var st = await _ApbdContext.Trips.OrderByDescending(x => x.DateFrom).ToListAsync();


            return Ok(st);
        }





        [HttpDelete("{idClient}")]
        public async Task<IActionResult> Delete(int idClient)
        {
            //clienttrips == 0;

            var a = await _ApbdContext.Clients.FindAsync(idClient);
            var b = _ApbdContext.ClientTrips.Where(x => x.IdClient == idClient);


            if (!(b.Any()))
            {
                _ApbdContext.Remove(a);
                _ApbdContext.SaveChangesAsync();

            }
            else
            {
                return BadRequest("Klient ma przypisaną wycieczke");

            }




            return Ok();

        }

        [Route("{idTrip}/clients")]
        [HttpPost]
        public IActionResult Post1(int idTrip)
        {
            var postt = _ApbdContext.Clients.

                Select(x => new
                {
                    name = x.LastName,
                    forname = x.LastName,
                    email = x.Email,
                    tel = x.Telephone,
                    pesel = x.Pesel,


                });



            return Ok();
        }


    }
}
