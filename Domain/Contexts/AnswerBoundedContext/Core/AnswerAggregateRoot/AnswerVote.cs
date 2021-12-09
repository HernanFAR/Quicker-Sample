using Quicker.Domain.Abstracts;
using System;

namespace Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot
{
    public class AnswerVote : Entity<Guid>
    {
        private AnswerVote() : base(Guid.NewGuid()) { }

        public AnswerVote(bool isUp, Guid votedBy) : this()
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
