using Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Quicker.Domain.DataAccess;
using Quicker.Domain.DataAccess.Repositories;
using System;

namespace Domain.DataAccess
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IRepositoryAsync<Question, Guid> Questions { get; }

        public IRepositoryAsync<Answer, Guid> Answers { get; }
    }
}
