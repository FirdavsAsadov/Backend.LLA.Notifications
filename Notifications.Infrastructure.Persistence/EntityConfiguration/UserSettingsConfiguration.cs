using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Infrastructure.Domain.Entiteis;

namespace Notifications.Infrastructure.Persistence.EntityConfiguration
{
    public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
    {
        public void Configure(EntityTypeBuilder<UserSettings> builder)
        {
            builder.HasOne<User>().WithOne(user => user.UserSettings)
                .HasForeignKey<UserSettings>(settings => settings.Id);
        }
    }
}
