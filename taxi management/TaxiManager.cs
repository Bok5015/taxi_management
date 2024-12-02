using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiManagementAssignment
{
    public class TaxiManager
    {
        private SortedDictionary<int, Taxi> taxis;

        public TaxiManager() 
        { 
            taxis = new SortedDictionary<int, Taxi>(); 
        }

        public Taxi CreateTaxi(int taxiNum) //taxi does not exist in the dictionary
        {
            if (!taxis.ContainsKey(taxiNum)) 
            {
                Taxi newTaxi = new Taxi(taxiNum);
                taxis[taxiNum] = newTaxi; 
                return newTaxi;
            }
            return taxis[taxiNum];

        }
        public Taxi FindTaxi(int taxiNum)
        {
            if (taxis.ContainsKey(taxiNum))
            {
                return taxis[taxiNum];// return correct taxi
            }
            return null;// cannot find taxi
        }
        public SortedDictionary<int, Taxi> GetAllTaxis()
        {
            return taxis;
        }
    }
}
