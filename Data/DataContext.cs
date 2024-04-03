

using ApiNet7.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiNet7.Data
{
    // Constructor
    public class DataContext: DbContext
    {  
        public DataContext(DbContextOptions<DataContext> options) 
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}