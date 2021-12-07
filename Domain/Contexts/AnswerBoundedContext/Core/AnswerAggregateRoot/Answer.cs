using System;
using System.Collections.Generic;
using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.ETOs;
using System.Linq;
using Domain.Contexts.AnswerBoundedContext.ETOs;
using Domain.Contexts.AnswerBoundedContext.Validators;
using Domain.Contexts.SharedBoundedContext.ValueObjects;
using Quicker.Domain;
using Quicker.Domain.Abstracts.Audited.AggregateRoots.CUDRAudited;
using Domain.Contexts.QuestionBoundedContext.Validators;
using FluentValidation;

namespace Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot
{
    // Acá, hay algo que mencionar
    // Si te fijas, en Question se usaron constructores para instanciar, esto es debido a que en la mayoría de los
    // casos es el modo más rápido y apropiado.
    //
    // Sin embargo, van a haber algunos casos en donde usar constructores puede ser tedioso, y no respetar
    // apropiadamente el lenguaje ubicuo (Ubicuous Language) por tener muchisimos parametros. En estos casos,
    // puedes usar un Builder para instanciar o modificar la instancia.
    //
    // Esta clase es pequeña y perfectamente pudo hacerse con constructor publico, pero por ejemplificar el uso de
    // un Builder, se hizo con este patrón.
    //
    public sealed class Answer : CUDRUserAuditedAggregateRoot<Guid, Guid?>
    {
        private Answer() : base(Guid.NewGuid()) { }

        private Answer(string name, Guid toQuestionId, Guid? toAnswerId, Guid createdBy) : this()
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

        public override void Validate()
        {
            var validator = new AnswerValidator();

            validator.ValidateAndThrow(this);
        }

        public EditBuilder EditAnswer(Guid updatedBy) => new(this, updatedBy);

        public class EditBuilder
        {
            internal readonly Answer _CurrentState;

            internal string _Name = string.Empty;
            internal Guid _UpdatedBy;

            internal EditBuilder(Answer answer, Guid updatedBy)
            {
                _CurrentState = answer;
                _UpdatedBy = updatedBy;
            }

            public EditBuilder SetNewName(string name)
            {
                _Name = name;

                return this;
            }
            
            public void SetValues()
            {
                _CurrentState.Name = _Name;

                _CurrentState.Update(_UpdatedBy);
            }
        }

        public class CreateBuilder
        {
            internal string _Name = string.Empty;
            internal Guid _CreatedBy;
            internal Guid _ToQuestion;
            internal Guid? _ToAnswer;

            public CreateBuilder(Guid createdBy)
            {
                _CreatedBy = createdBy;
            }

            public CreateBuilder WithName(string name)
            {
                _Name = name;

                return this;
            }

            public CreateBuilder ToQuestion(Guid questionId)
            {
                _ToQuestion = questionId;

                return this;
            }

            public CreateBuilder ToAnswer(Guid answerId)
            {
                _ToAnswer = answerId;

                return this;
            }

            public Answer Create() => 
                new(_Name, _ToQuestion, _ToAnswer, _CreatedBy);
        }
    }
}
