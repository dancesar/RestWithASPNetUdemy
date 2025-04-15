using DotNetEnv;
using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.model.Context
{
    public class MySQLContext : DbContext
    {
        /*public MySQLContext() {}*/
        
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

        public DbSet<Person> Persons { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}