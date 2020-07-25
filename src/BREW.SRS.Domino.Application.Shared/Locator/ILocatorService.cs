using BREW.SRS.Domino.Application.Shared.Locator.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BREW.SRS.Domino.Application.Shared.Locator
{
    public interface ILocatorService
    {
        Task<List<LocatorDto>> Get();
        Task<LocatorDto> Ensure(LocatorDto personDto);

        Task<LocatorDto> Delete(int id);
    }
}
