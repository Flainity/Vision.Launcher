using Microsoft.EntityFrameworkCore;
using Veritas.Library.Database.Schema.Account;

namespace Veritas.Library.Database.Context
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
