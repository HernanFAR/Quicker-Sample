using Domain.Contexts.UserBoundedContext.Builders;
using Domain.Contexts.UserBoundedContext.Constants;
using Domain.Contexts.UserBoundedContext.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore.Relations.UserBoundedContext
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly ApplicationDbContext _Context;

        public ApplicationUserEntityTypeConfiguration(ApplicationDbContext context)
        {
            _Context = context;
        }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable(nameof(ApplicationUser), UserDatabaseConstants.Schema);

            if (_Context.Database.IsSqlite())
            {
                builder.HasKey(e => e.Id);
            }
            else
            {
                builder.HasKey(e => e.Id)
                    .IsClustered(false);

                builder.Property<int>("ClusteredId")
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                builder.HasIndex("ClusteredId")
                    .IsUnique()
                    .IsClustered();
            }

            builder.Property(e => e.Name)
                .HasMaxLength(UserConstants.NameProperty.MaxLength)
                .IsRequired();

            builder.Property(e => e.UserName)
                .HasMaxLength(UserConstants.UserNameProperty.MaxLength)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(UserConstants.EmailProperty.MaxLength)
                .IsRequired();

            builder.Property(e => e.SubName)
                .HasMaxLength(UserConstants.SubNameProperty.MaxLength);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(UserConstants.PhoneNumberProperty.MaxLength);

            var user = ApplicationUserCreateBuilder.Create
                .WithId(UserConstants.AdminEntity.Id).WithEmail(UserConstants.AdminEntity.Email)
                .WithName(UserConstants.AdminEntity.Name).WithUserName(UserConstants.AdminEntity.UserName)
                .Continue
                .WithPhoneNumber(UserConstants.AdminEntity.PhoneNumber).WithSubName(UserConstants.AdminEntity.SubName)
                .GetInstance;

            user.NormalizedEmail = UserConstants.AdminEntity.Email.ToUpper();
            user.EmailConfirmed = true;
            user.NormalizedUserName = UserConstants.AdminEntity.UserName.ToUpper();
            user.PhoneNumberConfirmed = false;
            user.PasswordHash = "AKGEFunVW6jf5iMPDIVnGMDFxLV3V8zte65VXv0/k0HZx4QaGNcAix9tiJQqP1Qn+A==";
            user.SecurityStamp = "655ED840-2BE2-4090-A25E-CFC87502D559";
            user.ConcurrencyStamp = "452241B4-DEE8-4C04-B467-FE18FA73AAB0";
            user.LockoutEnabled = true;
            user.LockoutEnd = null;
            user.AccessFailedCount = 0;
            user.TwoFactorEnabled = false;

            builder.HasData(user);
        }
    }
}
