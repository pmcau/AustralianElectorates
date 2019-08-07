using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace AustralianElectorates.Bogus
{
    public partial class ElectorateDataSet : DataSet
    {
        public IEnumerable<string> Names(int num = 1)
        {
            Guard.AgainstNegative(num, nameof(num));
            for (var i = 0; i < num; i++)
            {
                yield return Name();
            }
        }

        public string Name()
        {
            return Electorate().Name;
        }

        public IEnumerable<Electorate> Electorate(int num = 1)
        {
            Guard.AgainstNegative(num, nameof(num));
            for (var i = 0; i < num; i++)
            {
                yield return Electorate();
            }
        }

        public Member CurrentMember()
        {
            var index = Random.Number(DataLoader.AllCurrentMembers.Count - 1);
            return DataLoader.AllCurrentMembers[index];
        }

        public IEnumerable<Member> CurrentMember(int num = 1)
        {
            Guard.AgainstNegative(num, nameof(num));
            for (var i = 0; i < num; i++)
            {
                yield return CurrentMember();
            }
        }

        public string CurrentMemberName()
        {
            return CurrentMember().FullName();
        }

        public IEnumerable<string> CurrentMemberName(int num = 1)
        {
            Guard.AgainstNegative(num, nameof(num));
            return CurrentMember(num).Select(x => x.FullName());
        }

        public Member Member()
        {
            var index = Random.Number(DataLoader.AllMembers.Count - 1);
            return DataLoader.AllMembers[index];
        }

        public IEnumerable<Member> Member(int num = 1)
        {
            Guard.AgainstNegative(num, nameof(num));
            for (var i = 0; i < num; i++)
            {
                yield return Member();
            }
        }

        public string MemberName()
        {
            return Member().FullName();
        }

        public IEnumerable<string> MemberName(int num = 1)
        {
            Guard.AgainstNegative(num, nameof(num));
            return Member(num).Select(x => x.FullName());
        }

        public Electorate Electorate()
        {
            var index = Random.Number(DataLoader.Electorates.Count - 1);
            return DataLoader.Electorates[index];
        }
    }
}