using CodeFirstApproachExample1.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstApproachExample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OurHeroController : ControllerBase
    {
        private readonly IOurHeroService _OurHeroService;
        public OurHeroController(IOurHeroService OurHeroService)
        {
            _OurHeroService = OurHeroService;
        }

        [HttpGet]
        //[Authorize(Roles ="Admin,Manager,SuperAdmin")]
        //[Authorize(Roles ="User")]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] bool? isActive = null)
        {
            var heros = await _OurHeroService.GetAllHeros(isActive);
            return Ok(heros);
        }
        [HttpGet("{id}")]
       // [Route("{id}")] // /api/OurHero/:id
        public async Task<IActionResult> Get(int id)
        {
            var hero = await _OurHeroService.GetHerosByID(id);
            if (hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUpdateOurHero heroObject)
        {
            var hero = await _OurHeroService.AddOurHero(heroObject);

            if (hero == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Super Hero Created Successfully!!!",
                id = hero!.Id
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] AddUpdateOurHero heroObject)
        {
            var hero = await _OurHeroService.UpdateOurHero(id, heroObject);
            if (hero == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Super Hero Updated Successfully!!!",
                id = hero!.Id
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!await _OurHeroService.DeleteHerosByID(id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Super Hero Deleted Successfully!!!",
                id = id
            });
        }
    }
}
    

