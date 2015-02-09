using System;
using System.Net;
using SOS.Data.GpsTracking;

namespace SOS.Lib.KW621API.Commands
{
	public class Response : IResponse
	{
		#region Commands
		public const string ATI4 = "ATI4";  // This is used to get the device model.
		#endregion Commands

		#region .ctor

		public Response(string rawSentence, CommandDef commandName = CommandDef.Undefined)
		{
			SentenceNet = rawSentence;
			CommandName = commandName;
		}

		#endregion .ctor

		#region Member Variables
		public CommandDef CommandName { get; private set; }
		public string SentenceNet { get; private set; }
		public KW_CommandMessage CommandMessage { get; private set; }

		#endregion Member Variables

		#region Member Functions

		public static object Factory(string sentence, EndPoint remoteEndPoint, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs)
		{
			return SaveSentance(sentence, remoteEndPoint, dispatchToCs, GetCommandObject);
		}

		private static object SaveSentance(string sentence, EndPoint remoteEndPoint, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs, Func<string, EndPoint, KW_CommandMessage, Func<string, decimal, decimal, string, string, bool?, bool>, object> action)
		{
			/** Initialize. */
			var commandMessage = new KW_CommandMessage { Sentence = sentence, MessageDate = DateTime.Now, CreatedOn = DateTime.Now, DEX_ROW_TS = DateTime.UtcNow };

			var command = action(sentence, remoteEndPoint, commandMessage, dispatchToCs);

			/** Return message object. */
			return command;
		}

		private static object GetCommandObject(string sentence, EndPoint remoteEndPoint, KW_CommandMessage commandMessage, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs)
		{
			/** Initialize. */
			var oResponse = new Response(sentence, CommandDef.HE910);

			if (sentence.IndexOf("HE910", StringComparison.Ordinal) == 0)
			{
				commandMessage.UnitID = (int)CommandDef.HE910;
			}
  
			/** Default Save information to commandMessage. */
			oResponse.SaveInfo(remoteEndPoint, commandMessage, forceSave: true);
			return oResponse;
		}

		public void SaveInfo(EndPoint remoteEndPoint, KW_CommandMessage commandMessage, bool forceSave = false)
		{
			/** BInd information to the Response. */
			commandMessage.IPAddress = ((IPEndPoint) (remoteEndPoint)).Address.ToString();
			commandMessage.Port = ((IPEndPoint) (remoteEndPoint)).Port;
			if (forceSave) commandMessage.Save();

			/** Get a handle on the command message for this base class. */
			CommandMessage = commandMessage;
		}

		public string GetResponseBack()
		{
			return string.Empty;
		}
		#endregion Member Functions
	}
}
