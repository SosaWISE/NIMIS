using System;
using AuthorizeNet;

namespace SampleAuthorization
{
	class Program
	{

		static void Main(string[] args)
		{

			//step 1 - create the request
			var request = new AuthorizationRequest("4111111111111111", "1216", 10.00M, "Test Transaction");

			//These are optional calls to the API
			request.AddCardCode("321");

			//Customer info - this is used for Fraud Detection
			request.AddCustomer("id", "first", "last", "address", "state", "zip");

			//order number
			request.AddInvoice("invoiceNumber");

			//Custom values that will be returned with the response
			request.AddMerchantValue("merchantValue", "AndresSosa MerchValue");

			//Shipping Address
			request.AddShipping("id", "first", "last", "address", "state", "zip");


			//step 2 - create the gateway, sending in your credentials and setting the Mode to Test (boolean flag)
			//which is true by default
			//this login and key are the shared dev account - you should get your own if you 
			//want to do more testing
			/** var gate = new Gateway("API-LOGIN", "TRANSACTION-KEY", true); */
			/** Live Test: var gate = new Gateway("4YdtR95C", "55xf8qhX88WXB5Us", false); */

			var gate = new Gateway("8sG6L4xs", "64T4EM362qvy2XN5", true);
			
			//step 3 - make some money
			var response = gate.Send(request);

			Console.WriteLine("{0}: {1}", response.ResponseCode, response.Message);
			Console.Read();
		}
	}
}
