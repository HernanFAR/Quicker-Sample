using Quicker.Domain.Abstracts;
using System;

namespace Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot
{
    public class QuestionVote : Entity<Guid>
    {
        private QuestionVote() : base(Guid.NewGuid()) { }

        public QuestionVote(bool isUp, Guid votedBy) : this()
        {
            By = votedBy;
            IsUp = isUp;
        }

        public Guid By { get; private set; }

        public bool IsUp { get; private set; }

        public void UpdateInfo(bool isUp)
        {
            IsUp = isUp;
        }
    }
}
