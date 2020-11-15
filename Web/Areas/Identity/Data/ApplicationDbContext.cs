using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Web.Areas.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<Assessment.Models.User, Assessment.Models.Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }

    }
}
