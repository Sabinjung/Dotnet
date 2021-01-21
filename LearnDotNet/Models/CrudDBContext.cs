using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnDotNet.Models
{
    public class CrudDBContext: DbContext
    {
        public CrudDBContext(DbContextOptions<CrudDBContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
