using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarChart.Controllers
{
    [Route(""),ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}",Name ="GetById")]
        public IActionResult GetById(int id)
        {
            var co = _context.CelestialObjects.Find(id);
            if (co != null)
            {
                co.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == co.Id).ToList();
                return Ok(co);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{name}", Name = "GetByName")]
        public IActionResult GetByName(string name)
        {
            var co = _context.CelestialObjects.Where(x=>x.Name==name).ToList();
            if (co != null && co.Count>0)
            {
                foreach (var c in co)
                {
                    c.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == c.Id).ToList();
                }
                return Ok(co);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll()
        {
            var co = _context.CelestialObjects.ToList();
            foreach (var c in co)
            {
                c.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == c.Id).ToList();
            }
            return Ok(co);
        }
    }
}
