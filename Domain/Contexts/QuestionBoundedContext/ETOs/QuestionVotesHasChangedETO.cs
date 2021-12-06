using System;

namespace Domain.Contexts.QuestionBoundedContext.ETOs
{
    public class QuestionVotesHasChangedETO
    {
        public QuestionVotesHasChangedETO(Guid questionId)
        {
            QuestionId = questionId;
        }

        public Guid QuestionId { get; private set; }
    }
}
