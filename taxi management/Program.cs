using System;
using System.Collections.Generic;
using System.ComponentModel;
using TaxiManagementAssignment;

namespace TaxiManagement
{
    class Program
    {
        private static UserUI ui;
        static void Main(string[] args)
        {
            //Console.WriteLine("\n\tWrite your Taxi Management application in this project." +
            //    "\n\n\tFollow the instructions in the assignment specification." +
            //    "\n\n\tREMEMBER: do not change any of the code in the tests project." +
            //    "\n\n\tYou can delete the code in the Main() method.\n\n");
            RankManager rankMgr = new RankManager();
            TaxiManager taxiMgr = new TaxiManager();
            TransactionManager transMgr = new TransactionManager(); 
            ui = new UserUI(rankMgr,taxiMgr,transMgr);

            DisplayMenu();
            
        }
        static void DisplayMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Taxi Management System");
                Console.WriteLine("------------------------");
                Console.WriteLine("1. Taxi Joins Rank");
                Console.WriteLine("2. Taxi Leaves Rank");
                Console.WriteLine("3. Taxi Drops Fare");
                Console.WriteLine("4. View Taxi Location");
                Console.WriteLine("5. View Financial Report");
                Console.WriteLine("6. View Transaction Log");
                Console.WriteLine("7. Exit");
                Console.WriteLine("------------------------");
                Console.Write("Choose an option:");

                int option = ReadInterger("");
                switch (option)
                {
                    case 1:
                        TaxiJoinsRank();
                        break;
                    case 2:
                        TaxiLeavesRank();
                        break;
                    case 3:
                        TaxiDropsFare();
                        break;
                    case 4:
                        ViewTaxiLocation();
                        break;
                    case 5:
                        ViewFinancialReport();
                        break;
                    case 6:
                        ViewTransactionLog();
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        static void DisplayResult(List<string> results)
        {
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
            Console.WriteLine("");
            Console.WriteLine("Press any key to return menu");
            Console.ReadKey();
        }
        static double ReadDouble(string prompt)
        {
            Console.WriteLine(prompt);
            return Convert.ToDouble(Console.ReadLine());
        }
        static int ReadInterger(string prompt)
        {
            Console.WriteLine(prompt);
            return Convert.ToInt32(Console.ReadLine());
        }
        static string ReadString(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
        static void TaxiDropsFare()
        {
            int taxiNum = ReadInterger("Enter taxi number: ");
            bool pricePaid = ReadString("Was the price paid? (y/n): ").ToLower() == "y";
            List<string> results = ui.TaxiDropsFare(taxiNum, pricePaid);
            DisplayResult(results);
        }
        static void TaxiJoinsRank()
        {
            int taxiNum = ReadInterger("Enter taxi number: ");
            int rankId = ReadInterger("Enter rank ID: ");
            List<string> results = ui.TaxiJoinsRank(taxiNum, rankId);
            DisplayResult(results);
        }
        static void TaxiLeavesRank()
        {
            int taxiNum = ReadInterger("Enter taxi number: ");
            string destination = ReadString("Enter the destination: ");
            double agreedPrice = ReadDouble("Enter agreed price: ");
            List<string> results = ui.TaxiLeavesRank(taxiNum, destination, agreedPrice);
            DisplayResult(results);
        }
        static void ViewFinancialReport()
        {
            List<string> results = ui.ViewFinancialReport();
            DisplayResult(results);
        }
        static void ViewTaxiLocation()
        {
            List<string> results = ui.ViewTaxiLocations();
            DisplayResult(results);
        }
        static void ViewTransactionLog()
        {
            List<string> results = ui.ViewTransactionLog();
            DisplayResult(results);
        }
    }

}
