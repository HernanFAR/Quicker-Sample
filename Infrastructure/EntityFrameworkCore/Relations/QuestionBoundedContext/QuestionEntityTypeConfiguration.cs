using Domain.Contexts.AnswerBoundedContext.Constants;
using Domain.Contexts.QuestionBoundedContext.Constants;
using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.SharedBoundedContext.ValueObjects;
using Domain.Contexts.UserBoundedContext.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.EntityFrameworkCore.Relations.QuestionBoundedContext
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        private readonly ApplicationDbContext _Context;

        public QuestionEntityTypeConfiguration(ApplicationDbContext context)
        {
            _Context = context;
        }

        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable(nameof(Question), QuestionDatabaseConstants.Schema);

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
                .HasMaxLength(AnswerConstants.NameProperty.MaxLength)
                .IsRequired();

            builder.ConfigureCreatedDate<Question, Guid?>();
            builder.ConfigureUpdatedDate<Question, Guid?>();
            builder.ConfigureDeactivatedDate<Question, Guid?>();
            builder.ConfigureReactivatedDate<Question, Guid?>();

            builder.OwnsOne(e => e.CurrentVotes, CurrentVoteEntityType);

            builder.OwnsMany(e => e.Votes, QuestionVoteEntityType);
        }

        private void CurrentVoteEntityType(OwnedNavigationBuilder<Question, VoteDetail> builder)
        {
            builder.Property(e => e.UpVotes)
                .IsRequired();

            builder.Property(e => e.DownVotes)
                .IsRequired();

            builder.Property(e => e.DeltaOfVotes)
                .IsRequired();
        }

        private void QuestionVoteEntityType(OwnedNavigationBuilder<Question, QuestionVote> builder)
        {
            builder.ToTable(nameof(QuestionVote), AnswerDatabaseConstants.Schema);

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

            builder.Property(e => e.IsUp)
                .IsRequired();

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(e => e.By)
                .IsRequired();
        }
    }
}
