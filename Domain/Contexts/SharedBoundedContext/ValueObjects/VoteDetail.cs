using Quicker.Domain.Abstracts;
using System.Collections.Generic;

namespace Domain.Contexts.SharedBoundedContext.ValueObjects
{
    public class VoteDetail : ValueObject
    {
        private VoteDetail() { }

        public VoteDetail(int upVotes, int downVotes) : this()
        {
            UpVotes = upVotes;
            DownVotes = downVotes;
        }

        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return UpVotes;
            yield return DownVotes;
            yield return DeltaOfVotes;
        }

        public int DeltaOfVotes
        {
            get => UpVotes - DownVotes;
            private set { }
        }

        public int UpVotes { get; private set; }

        public int DownVotes { get; private set; }
    }
}
