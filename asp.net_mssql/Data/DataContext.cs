using Microsoft.EntityFrameworkCore;
using Meaurse.Models;


namespace Meaurse.Data
{
     public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt):base(opt)
        {

        }
        public DbSet<Value> Meaurses { get; set; }
    }
}