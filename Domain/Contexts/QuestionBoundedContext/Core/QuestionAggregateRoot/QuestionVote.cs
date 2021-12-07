using System;
using Quicker.Domain.Abstracts;

namespace Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot
{
    public class QuestionVote : Entity<Guid>
    {
        public QuestionVote(bool isUp, Guid votedBy) : base(Guid.NewGuid())
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
