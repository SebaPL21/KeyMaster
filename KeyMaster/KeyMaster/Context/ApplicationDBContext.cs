using KeyMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyMaster.Context
{
    public class ApplicationDBContext :DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {}
        public virtual DbSet<UserModel> Users { get; set; }
    }
}
