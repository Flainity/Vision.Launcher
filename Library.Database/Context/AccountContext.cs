using Library.Database.Schema.Account;
using Microsoft.EntityFrameworkCore;

namespace Library.Database.Context
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
