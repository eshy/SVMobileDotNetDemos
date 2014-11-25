using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PicShare.Data.Entities;

namespace PicShare.Data.Context
{
    public class AuthContext : IdentityDbContext<Account>
    {
        public AuthContext() :base("DefaultConnection")
        {
            
        }

        public DbSet<Picture> Pictures { get; set; }
    }
}
