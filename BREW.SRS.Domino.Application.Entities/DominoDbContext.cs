using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BREW.SRS.Domino.Application.Entities
{
    public class DominoDbContext : DbContext
    {
        public DominoDbContext(DbContextOptions<DominoDbContext> options): base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }

    }
}
