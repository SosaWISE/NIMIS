using System;
using AuthorizeNet;

namespace SampleReporting
{
	class Program
	{
		static void Main(string[] args)
		{

			var gate = new ReportingGateway("APILOGIN", "TRANSACTIONKEY");

			//Get all the batches settled
			var batches = gate.GetSettledBatchList();

			Console.WriteLine("All Batches in the last 30 days");
			foreach (var item in batches)
			{
				Console.WriteLine("Batch ID: {0}, Settled On : {1}", item.ID, item.SettledOn.ToShortDateString());
			}

			Console.WriteLine("*****************************************************");
			Console.WriteLine();


			var transactions = gate.GetTransactionList();
			foreach (var item in transactions)
			{
				Console.WriteLine("Transaction {0}: Card: {1} for {2} on {3}", item.TransactionID, item.CardNumber, item.SettleAmount.ToString("C"), item.DateSubmitted.ToShortDateString());
			}
			Console.Read();

		}
	}
}
