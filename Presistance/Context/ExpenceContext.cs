using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistance.Context
{
    public class ExpenceContext : DbContext
    {
        public ExpenceContext(DbContextOptions<ExpenceContext> options) : base(options)
        {
        }
        public DbSet<Expense> Expenses { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
