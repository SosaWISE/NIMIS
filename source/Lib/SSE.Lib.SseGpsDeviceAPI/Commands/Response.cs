using System;
using System.Net;
using SOS.Data.GpsTracking;
using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.Helper;

namespace SSE.Lib.SseGpsDeviceAPI.Commands
{
	public class Response : IResponse
	{
		#region .ctor

		public Response(string sentence, CommandDef commandName)
		{
			CommandName = commandName;
			RawSentence = sentence;
		}

		#endregion .ctor

		#region Commands

		public const string SIR = "SIR";

		#endregion Commands

		#region Properties
		public CommandDef CommandName { get; private set; }
		public SS_CommandMessage CommandMessage { get; private set; }
		public string RawSentence { get; private set; }

		#endregion Properties


		#region Methods
		
		public string GetResponseBack()
		{
			return string.Empty;
		}

		public static object Factory(string sentence, EndPoint remoteEndPoint,
		                             Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs)
		{
			return SaveSentence(sentence, remoteEndPoint, dispatchToCs, GetCommandObject);
		}

		/// <summary>
		/// Saves the sentence to the raw SS_CommandMessage table
		/// </summary>
		/// <param name="sentence"></param>
		/// <param name="remoteEndPoint"></param>
		/// <param name="dispatchToCs"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		private static object SaveSentence(string sentence, EndPoint remoteEndPoint, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs, Func<string, EndPoint, SS_CommandMessage, Func<string, decimal, decimal, string, string, bool?, bool>, object> action)
		{
			// ** Save the raw command message.
			var commandMessage = new SS_CommandMessage
				{
					Sentence = sentence,
					CommandMessageTypeId = SS_CommandMessageType.MetaData.ClientResponseID,
					CommandMessageNameId = SentenceParser.GetCommandMessageNameID(sentence),
					IPAddress = ((IPEndPoint) (remoteEndPoint)).Address.ToString(),
					Port = ((IPEndPoint) (remoteEndPoint)).Port,
					MessageDate = DateTime.UtcNow,
					CreatedOn = DateTime.UtcNow
				};

			// ** Create command
			var command = action(sentence, remoteEndPoint, commandMessage, dispatchToCs);

			// ** Return message object.
			return command;
		}

		private static object GetCommandObject(string sentence, EndPoint remoteEndPoint, SS_CommandMessage commandMessage,
		                                       Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs)
		{
			// ** Check for SIR
			if (sentence.IndexOf(SIR, StringComparison.Ordinal) == 1)
			{
				var clientResponseSIR = new ClientResponseSIR(sentence);
				clientResponseSIR.SaveInfo(remoteEndPoint, commandMessage);
				return clientResponseSIR;
			}

			/** Default path of execution. */
			// ** Save unfound sentence. */
			var oResponse = new Response(sentence, SentenceParser.GetCommandName(sentence));
			oResponse.SaveInfo(remoteEndPoint, commandMessage, forceSave: true);
			return oResponse;
		}

		public void SaveInfo(EndPoint remoteEndPoint, SS_CommandMessage commandMessage, bool forceSave = false)
		{
			/** Bind information to the Response. */
			commandMessage.CommandMessageTypeId = SS_CommandMessageType.MetaData.ClientResponseID;
			commandMessage.IPAddress = ((IPEndPoint)(remoteEndPoint)).Address.ToString();
			commandMessage.Port = ((IPEndPoint)(remoteEndPoint)).Port;
			if (forceSave) commandMessage.Save();

			/** Get a handle on the command message for this base class. */
			CommandMessage = commandMessage;
		}

		protected void BindDeviceInfo(EndPoint remoteEndPoint, SS_Device lpDevice)
		{
			lpDevice.IPAddress = ((IPEndPoint)(remoteEndPoint)).Address.ToString();
			lpDevice.Port = ((IPEndPoint)(remoteEndPoint)).Port;
			lpDevice.LastAccessDate = DateTime.Now;
		}

		protected void SetCommandMessage(SS_CommandMessage commandMessage)
		{
			CommandMessage = commandMessage;
		}

		#endregion Methods
	}
}
