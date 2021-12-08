using Domain.Contexts.AnswerBoundedContext.Core.AnswerAggregateRoot;
using Domain.Contexts.AnswerBoundedContext.ETOs;
using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.ETOs;
using Domain.Contexts.SharedBoundedContext.ValueObjects;
using FluentAssertions;
using System;
using FluentValidation;
using Xunit;

namespace Domain.UnitTests.Contexts.AnswerBoundedContext.Core
{
    public class AnswerTests
    {
        [Fact]
        public void CreateAnswer_Success_Should_SetCreationInfo()
        {
            const string name = "name";
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();
            var answerId = Guid.NewGuid();

            var answer = new Answer(name, questionId, answerId, createdBy);

            answer.Id.Should().NotBe(default(Guid));
            answer.Name.Should().Be(name);
            answer.QuestionId.Should().Be(questionId);
            answer.CreatedBy.Should().Be(createdBy);
            answer.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(250));
        }

        [Fact]
        public void EditAnswer_Success_Should_SetNewValues()
        {
            const string name = "name";
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            var updatedBy = Guid.NewGuid();

            const string newName = "newName";

            var answer = new Answer(name, questionId, null, createdBy);

            var answerId = answer.Id;

            answer.UpdateInformation(newName, updatedBy);

            answer.Id.Should().Be(answerId);
            answer.Name.Should().Be(newName);
            answer.QuestionId.Should().Be(questionId);
            answer.CreatedBy.Should().Be(createdBy);
            answer.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(250));
            answer.UpdatedBy.Should().Be(updatedBy);
            answer.UpdatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(250));
        }

        [Fact]
        public void CurrentVotesProp_Success_Should_ReturnVotes()
        {
            const string name = "name";
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            var answer = new Answer(name, questionId, null, createdBy);

            VoteDetail details = answer.CurrentVotes;

            details.UpVotes.Should().Be(0);
            details.DownVotes.Should().Be(0);
            details.DeltaOfVotes.Should().Be(0);
        }

        [Fact]
        public void SetVote_Success_Should_AddNewVoteToQuestionAndRaiseVoteAddedETO_Detail_NonExistingVote()
        {
            const string name = "name";
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            var votedBy = Guid.NewGuid();
            const bool isUp = true;

            const int expCount = 1;
            const int expEventsCount = 1;

            var answer = new Answer(name, questionId, null, createdBy);

            answer.SetVote(votedBy, isUp);

            answer.GetEvents().Should().HaveCount(expEventsCount);
            answer.GetEvents().Should().ContainSingle(e => !e.AfterSave && ((AnswerVotesHasChangedETO)e.EventData).AnswerId == answer.Id);

            answer.Votes.Should().HaveCount(expCount);
            answer.Votes.Should().Contain(e => e.IsUp && e.By == votedBy && e.Id != default);
        }

        [Fact]
        public void SetVote_Success_Should_UpdateVoteToQuestionAndRaiseVoteAddedETO_Detail_ExistingVote()
        {
            const string name = "name";
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            var votedBy = Guid.NewGuid();
            const bool isUp = true;
            const bool newIsUp = false;

            const int expVotesCount = 1;
            const int expEventsCount = 2;

            var answer = new Answer(name, questionId, null, createdBy);

            answer.SetVote(votedBy, isUp);
            answer.SetVote(votedBy, newIsUp);

            answer.GetEvents().Should().HaveCount(expEventsCount);
            answer.GetEvents().Should().Contain(e => !e.AfterSave && ((AnswerVotesHasChangedETO)e.EventData).AnswerId == answer.Id);

            answer.Votes.Should().HaveCount(expVotesCount);
            answer.Votes.Should().Contain(e => !e.IsUp && e.By == votedBy && e.Id != default);
        }

        [Fact]
        public void UpdateVotes_Success_Should_UpdateVotes()
        {
            const string name = "name";
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            const int expUpVotes = 2;
            const int expDownVotes = 1;
            const int expDeltaOfVotes = expUpVotes - expDownVotes;

            var answer = new Answer(name, questionId, null, createdBy);

            answer.SetVote(Guid.NewGuid(), true);
            answer.SetVote(Guid.NewGuid(), true);
            answer.SetVote(Guid.NewGuid(), false);

            answer.UpdateVotes();

            VoteDetail details = answer.CurrentVotes;

            details.UpVotes.Should().Be(expUpVotes);
            details.DownVotes.Should().Be(expDownVotes);
            details.DeltaOfVotes.Should().Be(expDeltaOfVotes);
        }

        [Fact]
        public void Validate_Success_Should_PassFunction_Detail_ValidQuestion()
        {
            const string name = "TestString";
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            var answer = new Answer(name, questionId, null, createdBy);

            answer.Validate();

            true.Should().BeTrue();
        }

        [Fact]
        public void Validate_Failure_Should_PassFunction_Detail_ValidQuestion()
        {
            const string? name = null;
            var createdBy = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            const int expCount = 1;

            var answer = new Answer(name, questionId, null, createdBy);

            Action act = () => answer.Validate();

            act.Should().ThrowExactly<ValidationException>()
                .And.Errors.Should().HaveCount(expCount);
        }
    }
}
