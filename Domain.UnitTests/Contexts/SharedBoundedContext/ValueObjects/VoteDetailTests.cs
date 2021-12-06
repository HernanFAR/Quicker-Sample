using Domain.Contexts.SharedBoundedContext.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Contexts.SharedBoundedContext.ValueObjects
{
    public class VoteDetailTests
    {
        [Fact]
        public void DeltaOfVotes_Success_Should_ReturnDeltaOfVotes()
        {
            const int upVotes = 4;
            const int downVotes = 3;
            const int expDelta = 1;

            var voteDetail = new VoteDetail(upVotes, downVotes);

            voteDetail.DeltaOfVotes.Should().Be(expDelta);
        }

        [Fact]
        public void ValueEquals_Success_Should_ReturnTrue()
        {
            const int upVotesDetail1 = 4;
            const int downVotesDetail1 = 3;

            var voteDetail1 = new VoteDetail(upVotesDetail1, downVotesDetail1);
            var voteDetail2 = new VoteDetail(upVotesDetail1, downVotesDetail1);

            voteDetail1.ValueEquals(voteDetail2).Should().BeTrue();
        }

        [Fact]
        public void ValueEquals_Success_Should_ReturnFalse()
        {
            const int upVotesDetail1 = 4;
            const int downVotesDetail1 = 3;
            const int upVotesDetail2 = 3;
            const int downVotesDetail2 = 2;

            var voteDetail1 = new VoteDetail(upVotesDetail1, downVotesDetail1);
            var voteDetail2 = new VoteDetail(upVotesDetail2, downVotesDetail2);

            voteDetail1.ValueEquals(voteDetail2).Should().BeFalse();
        }
    }
}
