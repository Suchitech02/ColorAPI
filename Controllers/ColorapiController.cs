using Microsoft.AspNetCore.Mvc;
using ColorAPI.Entityframework;
using ColorAPI.Models;
using System.Collections.Generic;

namespace ColorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColorapiController : ControllerBase
    {
        private readonly ColorContext _context;
        public ColorapiController(ColorContext context)
        {
            _context = context;
        }

        [HttpGet("Colors")]
        public ActionResult<IEnumerable<Color>> GetColors()
        {
            return _context.colors;
        }
    }
}