using Domain.Contexts.AnswerBoundedContext.Constants;
using Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.SharedBoundedContext.ValueObjects;
using Domain.Contexts.UserBoundedContext.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore.Relations.AnswerBoundedContext
{
    public class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        private readonly ApplicationDbContext _Context;

        public AnswerEntityTypeConfiguration(ApplicationDbContext context)
        {
            _Context = context;
        }

        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable(nameof(Answer), AnswerDatabaseConstants.Schema);

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

            builder.HasOne<Question>().WithMany()
                .HasForeignKey(e => e.QuestionId)
                .IsRequired();

            builder.HasOne<Answer>().WithMany()
                .HasForeignKey(e => e.AnswerId)
                .IsRequired(false);

            builder.OwnsOne(e => e.CurrentVotes, CurrentVoteEntityType);

            builder.OwnsMany(e => e.Votes, AnswerVoteEntityType);
        }

        private void CurrentVoteEntityType(OwnedNavigationBuilder<Answer, VoteDetail> builder)
        {
            builder.Property(e => e.UpVotes)
                .IsRequired();

            builder.Property(e => e.DownVotes)
                .IsRequired();

            builder.Property(e => e.DeltaOfVotes)
                .IsRequired();
        }

        private void AnswerVoteEntityType(OwnedNavigationBuilder<Answer, AnswerVote> builder)
        {
            builder.ToTable(nameof(AnswerVote), AnswerDatabaseConstants.Schema);

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
            }

            builder.Property(e => e.IsUp)
                .IsRequired();

            builder.HasOne<ApplicationUser>().WithMany()
                .HasForeignKey(e => e.By)
                .IsRequired();
        }
    }
}
