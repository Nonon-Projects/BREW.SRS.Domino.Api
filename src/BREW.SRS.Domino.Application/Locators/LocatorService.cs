using BREW.SRS.Domino.Application.Entities;
using BREW.SRS.Domino.Application.Shared.Locator;
using BREW.SRS.Domino.Application.Shared.Locator.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
namespace BREW.SRS.Domino.Application.Locators
{
    public class LocatorService : ILocatorService
    {
        private readonly DominoDbContext _context;

        public LocatorService(DominoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LocatorDto> Delete(int id)
        {
            var locator = _context.Locators.FirstOrDefault(x => x.Id == id);
            var ret = new LocatorDto();
            if (locator != null)
            {
                _context.Locators.Remove(locator);
                await _context.SaveChangesAsync();
                ret.Id = id;
            }

            return ret;
        }

        public async Task<LocatorDto> Ensure(LocatorDto locatorDto)
        {
            var locator = new Locator()
            {
                HostName = locatorDto.HostName,
                Postal = locatorDto.Postal,
                Location = locatorDto.Location,
                CreationTime = DateTime.UtcNow,
                Country = locatorDto.Country,
                City = locatorDto.City,
                Ip = locatorDto.Ip,
                Org = locatorDto.Org,
                Region = locatorDto.Region
            };

            _context.Locators.Add(locator);
            await _context.SaveChangesAsync();
            locatorDto.Id = locator.Id;
            return locatorDto;
        }

        public async Task<List<LocatorDto>> Get()
        {
            var data = await _context.Locators.OrderBy(x => x.CreationTime).ToListAsync();
            var results = new List<LocatorDto>();

            foreach (var item in data)
            {
                results.Add(new LocatorDto()
                {
                    HostName = item.HostName,
                    Postal = item.Postal,
                    Location = item.Location,
                    Country = item.Country,
                    City = item.City,
                    Ip = item.Ip,
                    Org = item.Org,
                    Region = item.Region,
                    CreationTime = item.CreationTime,
                    Id = item.Id
                });
            }
            return results;
        }
    }
}
