using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagementAssignment
{
    public abstract class Transaction
    {
        public string TransactionType { get; private set; }
        
        public DateTime TransactionDatetime { get; private set; }

        protected Transaction(DateTime transactionDatetime, string transactionType)
        {
            TransactionDatetime = transactionDatetime;
            TransactionType = transactionType;
           
        }
        public DateTime GetTransactionDateTime() { return TransactionDatetime; }
        public string GetTransactionType() {  return TransactionType; }

        public override string ToString() //for the base in inherit class below to avoid DRY (Do not Repeat Yourself)
        {
            return $"{TransactionDatetime:dd/MM/yyyy HH:mm} {TransactionType}";
        }

    }
    
    public class JoinTransaction : Transaction
    {
        private int taxiNum;
        private int rankId;
        public JoinTransaction(DateTime transactionDatetime, int taxiNum, int rankId) : base(transactionDatetime, "Join")
        {
            this.taxiNum = taxiNum;
            this.rankId = rankId;
        }
        public override string ToString()
        {
            return $"{base.ToString()}      - Taxi {taxiNum} in rank {rankId}";
        }

    }

    public class LeaveTransaction : Transaction
    {
        private int taxiNum;
        private int rankId;
        private string destination;
        private double agreedPrice;
        public Taxi Taxi { get; private set; }

        public LeaveTransaction(DateTime transactionDatetime, int rankId, Taxi t) : base(transactionDatetime, "Leave")
        { 
            this.rankId = rankId; 
            Taxi = t;
            this.taxiNum = t.Number;
            this.destination = t.GetDestination();
            this.agreedPrice = t.GetCurrentFare();
        }
        public override string ToString()
        {
            return $"{base.ToString()}     - Taxi {taxiNum} from rank {rankId} to {destination} for £{agreedPrice}";
        }
    }
    public class DropTransaction : Transaction
    {
        private int taxiNum;
        private bool priceWasPaid;
        public DropTransaction(DateTime transactionDatetime, int taxiNum, bool priceWasPaid) : base(transactionDatetime, "Drop fare")
        {
            this.taxiNum = taxiNum;
            this.priceWasPaid = priceWasPaid;
        }
        public override string ToString()
        {
            if (priceWasPaid)
            {
                return $"{base.ToString()} - Taxi {taxiNum}, price was paid";
            }
            else
            {
                return $"{base.ToString()} - Taxi {taxiNum}, price was not paid";
            }
        }
    }

}
