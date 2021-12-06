using Domain.Contexts.QuestionBoundedContext.ETOs;
using Quicker.Domain;
using Quicker.Domain.Abstracts.Audited.AggregateRoots.CUDRAudited;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Contexts.QuestionBoundedContext.Validators;
using Domain.Contexts.SharedBoundedContext.ValueObjects;
using FluentValidation;

namespace Domain.Contexts.QuestionBoundedContext.Core
{
    public sealed class Question : CUDRUserAuditedAggregateRoot<Guid, Guid?>
    {
        private Question() { }

        public Question(string name, Guid createdBy) : base(Guid.NewGuid())
        {
            Name = name;

            Create(createdBy);
        }

        public string Name { get; private set; } = string.Empty;

        private readonly List<QuestionVote> _Votes = new();

        public IEnumerable<QuestionVote> Votes => _Votes;

        public VoteDetail CurrentVotes { get; private set; } = new VoteDetail(0, 0);

        public override void Validate()
        {
            var validator = new QuestionValidator();

            validator.ValidateAndThrow(this);
        }

        public void UpdateInfo(string newName, Guid updatedBy)
        {
            Name = newName;

            Update(updatedBy);
        }

        public void SetVote(Guid votedBy, bool isUp)
        {
            if (_Votes.Any(e => e.By == votedBy))
            {
                var vote = _Votes.Find(e => e.By == votedBy);

                vote.UpdateInfo(isUp);
            }
            else
            {
                _Votes.Add(new QuestionVote(isUp, votedBy));
            }

            AddEvent(new EventInformation(new QuestionVotesHasChangedETO(Id), false));
        }

        public void UpdateVotes()
        {
            var upVotes = _Votes.Count(e => e.IsUp);
            var downVotes = _Votes.Count(e => !e.IsUp);

            CurrentVotes = new VoteDetail(upVotes, downVotes);
        }
    }
}
