using BREW.SRS.Domino.Application.Shared.Locator;
using BREW.SRS.Domino.Application.Shared.Locator.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BREW.SRS.Domino.Host.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Locators")]
    public class LocatorsController : ControllerBase
    {
        private readonly ILocatorService _locatorService;

        public LocatorsController(ILocatorService locatorService)
        {
            _locatorService = locatorService ?? throw new ArgumentNullException(nameof(locatorService));
        }

        [HttpGet]
        public async Task<List<LocatorDto>> Get()
        {
            var data = await _locatorService.Get();

            return data;
        }
     
        [HttpPost("Ensure")]
        public async Task<LocatorDto> EnsureAsync(LocatorDto locatorDto)
        {
            return await _locatorService.Ensure(locatorDto);
        }


        [HttpDelete("Delete")]
        public async Task<LocatorDto> Delete(int id)
        {
            return await _locatorService.Delete(id);
        }

    }
}
