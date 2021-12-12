using Domain.Contexts.AnswerBoundedContext.ETOs;
using Domain.Contexts.AnswerBoundedContext.Validators;
using Domain.Contexts.SharedBoundedContext.ValueObjects;
using FluentValidation;
using Quicker.Domain;
using Quicker.Domain.Abstracts.Audited.AggregateRoots.CUDRAudited;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot
{
    public sealed class Answer : CUDRUserAuditedAggregateRoot<Guid, Guid?>
    {
        private Answer() : base(Guid.NewGuid())
        {
            Name = string.Empty;
        }

        public Answer(string name, Guid toQuestionId, Guid? toAnswerId, Guid createdBy) : this()
        {
            Name = name;
            QuestionId = toQuestionId;
            AnswerId = toAnswerId;

            Create(createdBy);
        }

        public string Name { get; private set; }

        public Guid QuestionId { get; private set; }

        public Guid? AnswerId { get; private set; }

        public VoteDetail CurrentVotes { get; private set; } = new(0, 0);

        private readonly List<AnswerVote> _Votes = new();

        public IEnumerable<AnswerVote> Votes => _Votes;

        public void SetVote(Guid votedBy, bool isUp)
        {
            if (_Votes.Any(e => e.By == votedBy))
            {
                var vote = _Votes.Find(e => e.By == votedBy);

                vote.UpdateInfo(isUp);
            }
            else
            {
                _Votes.Add(new AnswerVote(isUp, votedBy));
            }

            AddEvent(new EventInformation(new AnswerVotesHasChangedETO(Id), false));
        }

        public void UpdateVotes()
        {
            var upVotes = _Votes.Count(e => e.IsUp);
            var downVotes = _Votes.Count(e => !e.IsUp);

            CurrentVotes = new VoteDetail(upVotes, downVotes);
        }

        public void UpdateInformation(string name, Guid updatedBy)
        {
            Name = name;

            Update(updatedBy);
        }

        public override void Validate()
        {
            var validator = new AnswerValidator();

            validator.ValidateAndThrow(this);
        }
    }
}
