using Abp;
using Abp.Domain.Repositories;
using BREW.SRS.Domino.Application.Entities;
using BREW.SRS.Domino.Application.Shared.Persons;
using BREW.SRS.Domino.Application.Shared.Persons.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BREW.SRS.Domino.Application
{
    public class PersonService : IPersonService
    {
        private readonly DominoDbContext _context;
       // private readonly IRepository<Person> _personRepository;

        public PersonService(DominoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<PersonDto>> Get()
        {
            var data = await _context.Persons.ToListAsync();
            var results = new List<PersonDto>();

            foreach (var item in data)
            {
                results.Add(new PersonDto() { 
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MiddleName = item.MiddleName
                });
            }
            return results;
        }

        public async Task<PersonDto> Ensure(PersonDto personDto)
        {

            //BlobStorageService objBlobService = new BlobStorageService();

            var person = new Person()
            {
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                MiddleName = personDto.MiddleName
            };
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            personDto.Id = person.Id;
            return personDto;
        }

        public async Task<PersonDto> Delete(PersonDto personDto)
        {
             _context.Persons.Remove(new Person() { Id = (int)personDto.Id });
            await _context.SaveChangesAsync();
            return personDto;
        }
    }
}
