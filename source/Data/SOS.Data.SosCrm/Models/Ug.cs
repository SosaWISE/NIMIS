using System.Runtime.Serialization;

namespace SOS.Data.SosCrm.Models
{
	[DataContract]
	public partial class Ug
	{
		public Ug()
		{
			DocTypeID = 1;
		}
		#region Enum

		[DataContract]
		public enum UgEnum : int
		{
			[EnumMember()]
			Employee_Registration_Form = 1,
		}

		[DataMember]
		public UgEnum DocTypeCode
		{
			get { return (UgEnum)DocTypeID; }
			set { ; }
		}

		[DataMember]
		public int DocTypeID { get; set; }

		#endregion //Enum
	}
}
