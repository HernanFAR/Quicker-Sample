using System;

namespace Domain.Contexts.AnswerBoundedContext.ETOs
{
    public class AnswerVotesHasChangedETO
    {
        public AnswerVotesHasChangedETO(Guid answerId)
        {
            AnswerId = answerId;
        }

        public Guid AnswerId { get; private set; }
    }
}
