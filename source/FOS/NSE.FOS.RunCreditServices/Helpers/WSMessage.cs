using System.Collections.Generic;
using System.Text;

namespace NSE.FOS.RunCreditServices.Helpers
{
	public static class WSMessage
	{
		public static string PartitionList(List<SOS.Lib.Core.CreditReportService.WSMessage> messageList)
		{
			/** Initialize. */
			var sb = new StringBuilder();

			foreach (var wsMessage in messageList)
			{
				if (sb.Length == 0)
					sb.Append(wsMessage.DisplayMessage);
				else
					sb.AppendFormat("[|]{0}", wsMessage);
			}
			
			return sb.ToString();
		}
	}
}
