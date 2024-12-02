using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagementAssignment
{
    public class Rank
    {
        
        public int Id { get; private set; }
        public int NumberOfTaxiSpaces { get; private set; }
        private List<Taxi> TaxiSpace;
        public Rank(int rankId, int numberOfTaxiSpaces)
        {
            Id = rankId;
            NumberOfTaxiSpaces = numberOfTaxiSpaces;
            TaxiSpace = new List<Taxi>();
        }
        public bool AddTaxi(Taxi t)
        {
            if (TaxiSpace.Count >= NumberOfTaxiSpaces)
            {
                return false; //cannot add taxi if rank is full
            }
            else
            {
                TaxiSpace.Add(t);
                t.Rank = this;
                t.Location = Taxi.IN_RANK;
                return true;
            } 
        }
        public Taxi FrontTaxiTakesFare(string destination, double agreedPrice)
        {
            if (TaxiSpace.Count == 0)
            {
                return null; //cannot take fare if there is no taxi in rank
            }
            Taxi frontTaxi = TaxiSpace[0];
            TaxiSpace.RemoveAt(0); //remove front taxi in the rank
            frontTaxi.AddFare(destination, agreedPrice);
            return frontTaxi;
        }
        public int GetId() { return Id; }

    }
}
