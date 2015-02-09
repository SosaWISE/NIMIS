/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 17:52
 * 
 * Description:  Session Model information returned for a created instance of a handshake.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;
using SOS.Data.AuthenticationControl;
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models
{
	[DataContract]
	public class FnsAcSessionModel : IFnsAcSessionModel
	{
		#region .ctor
		public FnsAcSessionModel (long lSessionID, string szApplicationId, string szIPAddress, DateTime oCreatedDate, int? nUserId = null, DateTime dLastAccessedOn = default(DateTime), bool bSessionTerminated = false)
		{
			SessionID = lSessionID;
			ApplicationId = szApplicationId;
			UserId = nUserId;
			IPAddress = szIPAddress;
			LastAccessedOn = dLastAccessedOn;
			SessionTerminated = bSessionTerminated;
			CreatedOn = oCreatedDate;
		}

		public FnsAcSessionModel (AC_Session oSession)
		{
			SessionID = oSession.SessionID;
			ApplicationId = oSession.ApplicationId;
			UserId = oSession.UserId;
			IPAddress = oSession.IPAddress;
			LastAccessedOn = oSession.LastAccessedOn;
			SessionTerminated = oSession.SessionTerminated;
			CreatedOn = oSession.CreatedOn;
		}

		#endregion .ctor

		#region Properties
		[DataMember]
		public long SessionID { get; private set; }

		[DataMember]
		public string ApplicationId { get; private set; }

		[DataMember]
		public int? UserId { get; private set; }

		[DataMember]
		public string IPAddress { get; private set; }

		[DataMember]
		public DateTime LastAccessedOn { get; private set; }

		[DataMember]
		public bool SessionTerminated { get; private set; }

		[DataMember]
		public DateTime CreatedOn { get; private set; }

		#endregion Properties

	}
}