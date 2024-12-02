using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TaxiManagementAssignment
{
    public class Taxi
    {
        public static string IN_RANK = "in rank";
        public static string ON_ROAD = "on the road";
       
        public int Number { get; private set; }

        public double CurrentFare { get; private set; }    
        public string Destination { get; private set; }
        
        public string Location { get; set; } 
        
        public double TotalMoneyPaid { get; private set; }
        //private set can only change in the class
        private Rank rank;
        public Rank Rank
        {
            get
            {
                return rank;
            }
            set
            {
                rank = value;
                if (value == null)
                {
                    throw new Exception("Rank cannot be null"); 
                }
                if (Destination != string.Empty)
                {
                    throw new Exception("Cannot join rank if fare has not been dropped"); //cannot join rank if on the road to destination 
                }
                if (value != null) 
                {
                    Location = IN_RANK;
                }
                else
                {
                    Location = null;
                }
            }
        }

        public Taxi(int num) //set value for taxi
        {
            Number = num;
            CurrentFare = 0;
            Destination = string.Empty;
            Location = ON_ROAD;
            TotalMoneyPaid = 0;
        }
        public void AddFare(string destination, double agreedPrice)
        {
            Destination = destination;
            CurrentFare += agreedPrice; //add agreed price to current fare 
            Location = ON_ROAD;
            rank = null; //go out the rank when have destination
        }
        public void DropFare(bool priceWasPaid)
        { 
            if (priceWasPaid)
            { 
                TotalMoneyPaid += CurrentFare;//add current fare to total money paid 
            }
            else
            {
                TotalMoneyPaid = 0;
            }
            Destination = string.Empty;//return empty destination after dropping fare 
            CurrentFare = 0;// reset current fare after dropping fare
        }
        public double GetCurrentFare() { return CurrentFare; }
        public string GetDestination() { return Destination; }
        public string GetLocation() { return Location; }
        public int GetNumber() { return Number; }
        public Rank GetRank() { return Rank; }
        public double GetTotalMoneyPaid() { return TotalMoneyPaid; }
        public void SetRank(Rank r) { this.Rank = r; }

    }
}
