using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsOptionItem
	{
		[DataMember]
		string Value { get; set; }

		[DataMember]
		string Text { get; set; }
	}
}
