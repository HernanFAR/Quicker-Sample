using MediatR;
using System;

namespace Domain.Contexts.QuestionBoundedContext.ETOs
{
    public class QuestionVotesHasChangedETO : INotification
    {
        public QuestionVotesHasChangedETO(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; private set; }
    }
}
