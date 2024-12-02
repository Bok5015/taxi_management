using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagementAssignment
{
    public class UserUI
    {
        private RankManager rankMgr;
        private TaxiManager taxiMgr;
        private TransactionManager transactionMgr;
        public UserUI(RankManager rkMgr, TaxiManager txMgr, TransactionManager trMgr)
        {
            transactionMgr = trMgr;
            rankMgr = rkMgr;
            taxiMgr = txMgr;
        }
        public List<string> TaxiJoinsRank(int taxiNum, int rankId)
        {
            List<string> expectedLines = new List<string>();
            Taxi t = taxiMgr.FindTaxi(taxiNum);

            if (t == null) // no taxi
            {
                t = taxiMgr.CreateTaxi(taxiNum);
            }
            if (rankMgr.AddTaxiToRank(t, rankId)) // add existing taxi to rank
            {
                expectedLines.Add($"Taxi {taxiNum} has joined rank {rankId}.");
                transactionMgr.RecordJoin(taxiNum, rankId);
            }
            else
            {
                expectedLines.Add($"Taxi {taxiNum} has not joined rank {rankId}.");
            }
            return expectedLines;
        }

        public List<string> TaxiLeavesRank(int rankId, string destination, double agreedPrice)
        {
            List<string> expectedLines = new List<string>();
            Rank rank = rankMgr.FindRank(rankId); //find rank that have taxi leave 
            if (rank != null) // if rank exist
            {
                Taxi t = rankMgr.FrontTaxiInRankTakesFare(rankId, destination, agreedPrice);
                if (t != null) // make front taxi leave rank after takes fare
                {
                    expectedLines.Add($"Taxi {t.GetNumber()} has left rank {rankId} to take a fare to {destination} for £{agreedPrice}.");
                    transactionMgr.RecordLeave(rankId, t);
                }
                else
                {
                    expectedLines.Add($"Taxi has not left rank {rankId}.");
                }   
            }
            return expectedLines;
        }

        public List<string> TaxiDropsFare(int taxiNum, bool pricePaid)
        {
            List<string> expectedLines = new List<string>();
            Taxi t = taxiMgr.FindTaxi(taxiNum);
            if (t != null)
            {
                if (t.Destination == string.Empty) //cannot drop fare when taxi is on the road to destination 
                {
                    expectedLines.Add($"Taxi {taxiNum} has not dropped its fare.");
                }
                else
                {
                    t.DropFare(pricePaid);
                    if (!pricePaid) 
                    {
                        expectedLines.Add($"Taxi {taxiNum} has dropped its fare and the price was not paid.");
                    }
                    else
                    {
                        expectedLines.Add($"Taxi {taxiNum} has dropped its fare and the price was paid.");   
                    }
                    transactionMgr.RecordDrop(taxiNum, pricePaid);
                }
            }
            return expectedLines;
        }
        public List<string> ViewTaxiLocations()
        {
            List<string> expectedLines = new List<string>();
            expectedLines.Add("Taxi locations");
            expectedLines.Add("==============");

            var taxis = taxiMgr.GetAllTaxis();

            if (taxis.Count == 0)
            {
                expectedLines.Add("No taxis");
            }
            else
            {
                foreach (var t in taxis.Values) //take variable from taxis dictionary 
                {
                    if (t.GetLocation() == Taxi.IN_RANK) // taxi is in rank
                    {
                        expectedLines.Add($"Taxi {t.GetNumber()} is in rank {t.Rank.GetId()}");
                    }
                    else if (t.Destination != string.Empty) // taxi on road to its destination
                    {
                        expectedLines.Add($"Taxi {t.GetNumber()} is on the road to {t.GetDestination()}");
                    }
                    else
                    {
                        expectedLines.Add($"Taxi {t.GetNumber()} is on the road");
                    }
                }  
            }
            return expectedLines;
        }

        public List<string> ViewFinancialReport()
        {
            List<string> expectedLines = new List<string>();
            expectedLines.Add("Financial report");
            expectedLines.Add("================");
            
            var taxis = taxiMgr.GetAllTaxis();
            
            if (taxis.Count == 0)
            {
                expectedLines.Add("No taxis, so no money taken");
            }
            else
            {
                double totalFare = 0.0; // make a variable to add up money
                foreach (var t in taxis.Values)
                {
                    expectedLines.Add($"Taxi {t.GetNumber()}      {t.GetTotalMoneyPaid():F2}");
                    totalFare += t.GetTotalMoneyPaid();
                }
                expectedLines.Add("           ======");
                expectedLines.Add($"Total:       {totalFare:F2}");//F2 means display 2 zero after dot(.)
                expectedLines.Add("           ======");

            }
            return expectedLines;
        }

        public List<string> ViewTransactionLog()
        {
            List<string> expectedLines = new List<string>();
            expectedLines.Add("Transaction report");
            expectedLines.Add("==================");

            var transactions = transactionMgr.GetAllTransactions();

            if (transactions.Count == 0)
            {
                expectedLines.Add("No transactions");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    expectedLines.Add(transaction.ToString());// take messages from transaction
                }
            }
            return expectedLines;
        }
    }
}
