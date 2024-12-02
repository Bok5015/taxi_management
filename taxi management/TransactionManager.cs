using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TaxiManagementAssignment
{
    public class TransactionManager
    {
        private List<Transaction> transactions;
        public TransactionManager()
        {
            transactions = new List<Transaction>();
        }
        public List<Transaction> GetAllTransactions()
        {
            return transactions;
        }
        public void RecordJoin(int taxiNum, int rankId)
        {
            Transaction transaction = new JoinTransaction(DateTime.Now, taxiNum, rankId);
            transactions.Add(transaction);
        }
        public void RecordLeave(int rankId, Taxi t)
        {
            Transaction transaction = new LeaveTransaction(DateTime.Now, rankId, t);
            transactions.Add(transaction);
        }
        public void RecordDrop(int taxiNum, bool pricePaid)
        {
            Transaction transaction = new DropTransaction(DateTime.Now, taxiNum, pricePaid);
            transactions.Add(transaction);
        }
    }
}
