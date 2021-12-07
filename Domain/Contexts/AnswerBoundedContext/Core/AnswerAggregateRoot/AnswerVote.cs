using System;
using Quicker.Domain.Abstracts;

namespace Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot
{
    public class AnswerVote : Entity<Guid>
    {
        public AnswerVote(bool isUp, Guid votedBy) : base(Guid.NewGuid())
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
