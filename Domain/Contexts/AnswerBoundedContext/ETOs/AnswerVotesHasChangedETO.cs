using System;
using MediatR;

namespace Domain.Contexts.AnswerBoundedContext.ETOs
{
    public class AnswerVotesHasChangedETO : INotification
    {
        public AnswerVotesHasChangedETO(Guid answerId)
        {
            AnswerId = answerId;
        }

        public Guid AnswerId { get; private set; }
    }
}
