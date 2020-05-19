using Microsoft.EntityFrameworkCore;
using MOH.Common.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOH.Common.Data
{
    public class MOHContext : DbContext
    {
        public MOHContext(DbContextOptions<MOHContext> options) : base(options)
        {

        }


        public DbSet<Person> People { get; set; }

    }
}
