using Domain.Contexts.QuestionBoundedContext.Core.QuestionAggregateRoot;
using Domain.Contexts.QuestionBoundedContext.ETOs;
using Domain.Contexts.SharedBoundedContext.ValueObjects;
using FluentAssertions;
using FluentValidation;
using System;
using Xunit;

namespace Domain.UnitTests.Contexts.QuestionBoundedContext.Core
{
    public class QuestionTests
    {
        [Fact]
        public void Constructor_Success_Should_SetCreationInfo()
        {
            const string name = "TestString";
            var createdBy = Guid.NewGuid();

            var question = new Question(name, createdBy);

            question.Id.Should().NotBe(default(Guid));
            question.Name.Should().Be(name);
            question.CreatedBy.Should().Be(createdBy);
            question.CreatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(250));
        }

        [Fact]
        public void UpdateInfo_Success_Should_UpdateInfoOfQuestion()
        {
            const string name = "TestString";
            var createdBy = Guid.NewGuid();

            const string newName = "TestString2";
            var updatedBy = Guid.NewGuid();

            var question = new Question(name, createdBy);

            question.UpdateInfo(newName, updatedBy);

            question.Name.Should().Be(newName);
            question.UpdatedBy.Should().Be(updatedBy);
            question.UpdatedDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(250));
        }

        [Fact]
        public void CurrentVotesProp_Success_Should_ReturnVotes()
        {
            const string name = "TestString";
            var createdBy = Guid.NewGuid();

            var question = new Question(name, createdBy);

            VoteDetail details = question.CurrentVotes;

            details.UpVotes.Should().Be(0);
            details.DownVotes.Should().Be(0);
            details.DeltaOfVotes.Should().Be(0);
        }

        [Fact]
        public void SetVote_Success_Should_AddNewVoteToQuestionAndRaiseVoteAddedETO_Detail_NonExistingVote()
        {
            const string name = "TestString";
            var createdBy = Guid.NewGuid();

            var votedBy = Guid.NewGuid();
            const bool isUp = true;

            const int expCount = 1;
            const int expEventsCount = 1;

            var question = new Question(name, createdBy);

            question.SetVote(votedBy, isUp);

            question.GetEvents().Should().HaveCount(expEventsCount);
            question.GetEvents().Should().ContainSingle(e => !e.AfterSave && ((QuestionVotesHasChangedETO)e.EventData).QuestionId == question.Id);

            question.Votes.Should().HaveCount(expCount);
            question.Votes.Should().Contain(e => e.IsUp && e.By == votedBy && e.Id != default);
        }

        [Fact]
        public void SetVote_Success_Should_UpdateVoteToQuestionAndRaiseVoteAddedETO_Detail_ExistingVote()
        {
            const string name = "TestString";
            var createdBy = Guid.NewGuid();

            var votedBy = Guid.NewGuid();
            const bool isUp = true;
            const bool newIsUp = false;

            const int expVotesCount = 1;
            const int expEventsCount = 2;

            var question = new Question(name, createdBy);
            question.SetVote(votedBy, isUp);
            question.SetVote(votedBy, newIsUp);

            question.GetEvents().Should().HaveCount(expEventsCount);
            question.GetEvents().Should().Contain(e => !e.AfterSave && ((QuestionVotesHasChangedETO)e.EventData).QuestionId == question.Id);

            question.Votes.Should().HaveCount(expVotesCount);
            question.Votes.Should().Contain(e => !e.IsUp && e.By == votedBy && e.Id != default);
        }

        [Fact]
        public void UpdateVotes_Success_Should_UpdateVotes()
        {
            const string name = "TestString";
            var createdBy = Guid.NewGuid();

            const int expUpVotes = 2;
            const int expDownVotes = 1;
            const int expDeltaOfVotes = expUpVotes - expDownVotes;

            var question = new Question(name, createdBy);
            question.SetVote(Guid.NewGuid(), true);
            question.SetVote(Guid.NewGuid(), true);
            question.SetVote(Guid.NewGuid(), false);

            question.UpdateVotes();

            VoteDetail details = question.CurrentVotes;

            details.UpVotes.Should().Be(expUpVotes);
            details.DownVotes.Should().Be(expDownVotes);
            details.DeltaOfVotes.Should().Be(expDeltaOfVotes);
        }

        [Fact]
        public void Validate_Success_Should_PassFunction_Detail_ValidQuestion()
        {
            const string name = "TestString";
            var createdBy = Guid.NewGuid();

            var question = new Question(name, createdBy);

            question.Validate();

            true.Should().BeTrue();
        }

        [Fact]
        public void Validate_Failure_Should_PassFunction_Detail_ValidQuestion()
        {
            const string? name = null;
            var createdBy = Guid.NewGuid();

            const int expCount = 1;

            var question = new Question(name!, createdBy);

            Action act = () => question.Validate();

            act.Should().ThrowExactly<ValidationException>()
                .And.Errors.Should().HaveCount(expCount);
        }
    }
}
