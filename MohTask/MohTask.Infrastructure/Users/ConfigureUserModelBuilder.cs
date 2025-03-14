using Microsoft.EntityFrameworkCore;
using MohTask.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohTask.Infrastructure.Users
{
    public static class UserModelBuilderConfiguration
    {
        public static void ConfigureUserModelBuilder(this ModelBuilder builder)
        {
            // Configure primary key
            builder.Entity<User>().HasKey(u => u.Id);

            // Configure properties
            builder.Entity<User>()
               .Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(100);

            builder.Entity<User>()
              .Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(255);

            builder.Entity<User>()
                .Property(u => u.Status)
                  .HasDefaultValue(true);

        }
    }

}
