using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    internal struct GroupingAccOwnerIsInvestment : IComparable
    {
        public string _GroupingKey;
        public string _AccountOwner;
        public byte _IsInvestment;

        public GroupingAccOwnerIsInvestment(string groupingKey, string accountOwner, byte isInvestment)
        {
            _GroupingKey = groupingKey;
            _AccountOwner = accountOwner;
            _IsInvestment = isInvestment;
        }

        public int CompareTo(object obj)
        {
            GroupingAccOwnerIsInvestment other = (GroupingAccOwnerIsInvestment)obj;
            int res1 = _GroupingKey.CompareTo(other._GroupingKey);
            if (res1 == 0)
            {
                int res2 = _AccountOwner.CompareTo(other._AccountOwner);
                if (res2 == 0)
                    return _IsInvestment.CompareTo(other._IsInvestment);
                else
                    return res2;
            }
            else
                return res1;
        }
    }
}
