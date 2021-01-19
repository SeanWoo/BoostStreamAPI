using BoostStreamServer.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoostStreamServer.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "Moderator",
                    NormalizedName = "Moderator".ToUpper()
                }
            );
        }
    }
}
