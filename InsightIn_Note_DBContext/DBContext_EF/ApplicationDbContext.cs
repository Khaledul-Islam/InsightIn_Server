using InsightIn_Note_Model.Note;
using InsightIn_Note_Model.User;
using InsightIn_Note_Utilities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_DBContext.DBContext_EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<OTPService> OTPServices { get; set; }
        public virtual DbSet<NoteCat> NoteCategories { get; set; }
    }
}
