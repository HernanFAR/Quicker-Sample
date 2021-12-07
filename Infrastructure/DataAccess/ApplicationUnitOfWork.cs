using System;
using Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using Quicker.Domain.DataAccess.Repositories;
using Quicker.Infrastructure.EntityFramework.Abstracts;

namespace Infrastructure.DataAccess
{
    public class ApplicationUnitOfWork : EntityFrameworkUnitOfWork, IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(DbContext context, IRepositoryAsync<Question, Guid> questions, IRepositoryAsync<Answer, Guid> answers) :
            base(context)
        {
            Questions = questions;
            Answers = answers;
        }

        protected override object[] AllRepositories()
            => new object[] { Questions, Answers };

        public IRepositoryAsync<Question, Guid> Questions { get; }

        public IRepositoryAsync<Answer, Guid> Answers { get; }
    }
}
