using BREW.SRS.Domino.Application.Shared.Persons.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BREW.SRS.Domino.Application.Shared.Persons
{
    public interface IPersonService
    {
        Task<List<PersonDto>> Get();
        Task<PersonDto> Ensure(PersonDto personDto);
        Task<PersonDto> Delete(PersonDto personDto);
    }
}
