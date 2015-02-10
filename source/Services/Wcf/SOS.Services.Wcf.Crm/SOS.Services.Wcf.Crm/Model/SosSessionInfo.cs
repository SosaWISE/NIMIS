/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 17:23
 * 
 * Description:  Entity that contains session information.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;
using SOS.FunctionalServices.Contracts.Models;
using SOS.Services.Interfaces.Models;

namespace SOS.Services.Wcf.Crm.Model
{
	[DataContract]
	public class SosSessionInfo : ISosSessionInfo
	{
		#region .ctor
		public SosSessionInfo(ISessionModel oSessionModel)
		{
			SessionId = oSessionModel.SessionID;
			ApplicationId = oSessionModel.ApplicationId;
			IPAddress = oSessionModel.IPAddress;
			CreatedDate = oSessionModel.CreatedOn;
		}
		#endregion .ctor

		#region Properties

		[DataMember]
		public long SessionId { get; set; }

		[DataMember]
		public string ApplicationId { get; set; }

		[DataMember]
		public string IPAddress { get; set; }

		[DataMember]
		public DateTime CreatedDate { get; set; }

		#endregion Properties
	}
}