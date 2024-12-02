using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagementAssignment
{
    public class RankManager
    {
        private Dictionary<int, Rank> ranks;
        

        public RankManager()
        {
            ranks = new Dictionary<int, Rank>();
            ranks[1] = new Rank(1, 5);
            ranks[2] = new Rank(2, 2);
            ranks[3] = new Rank(3, 4);
        }//3 different rank 
        public bool AddTaxiToRank(Taxi t, int rankId)
        {   
            if (t.Rank != null) //if taxi already in rank
            {
                return false; 
            }
            if (t.Destination != string.Empty) //taxi already on the road -> cannot go in rank
            {
                return false;
            }
            if (ranks.TryGetValue(rankId, out Rank rank)) // get rankId from class Rank
            {
                return rank.AddTaxi(t);
            }
            return false;
        }
        public Rank FindRank(int rankId)
        {
            if (ranks.ContainsKey(rankId)) // rankId exist from dictionary
            {
                return ranks[rankId];
            }
            return null;
            
        }
        public Taxi FrontTaxiInRankTakesFare(int rankId, string destination, double agreedPrice)
        {
            if (ranks.TryGetValue(rankId, out Rank rank))
            {
                return rank.FrontTaxiTakesFare(destination, agreedPrice);
            }
            return null;
        }
    }
} 
    

    

