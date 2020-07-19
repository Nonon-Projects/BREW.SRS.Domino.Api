using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREW.SRS.Domino.Host.Controllers
{
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public async Task<List<string>> Get()
        {
            return new List<string>() { "A", "B", "C" };
        }
    }
}
