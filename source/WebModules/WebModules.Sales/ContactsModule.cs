using Nancy.ModelBinding;
using NXS.Data.Sales;
using NXS.DataServices.Sales;
using NXS.DataServices.Sales.Models;
using NXS.Lib.Web;
using NXS.Lib.Web.Authentication;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules.Sales
{
	public class ContactsModule : BaseModule
	{
		ContactsService Srv { get { return new ContactsService(/*this.User.GPEmployeeID*/); } }

		public ContactsModule()
			//: base("/Contact", "/ng")
			: base("/Sales/Contacts")
		{
			////$http.post('ng/Contact/save_contact', {
			//Post["/save_contact", true] = async (x, ct) =>
			Post["/", true] = async (x, ct) =>
			{
				var salesRepId = UsersModule.USERID;

				var inputItem = this.BindBody<ContactInput>();

				var contact = new SalesContact();
				contact.id = inputItem.id;
				contact.latitude = inputItem.latitude;
				contact.longitude = inputItem.longitude;

				SalesContactNote note = null;
				if (!string.IsNullOrEmpty(inputItem.address))
				{
					note = new SalesContactNote();
					note.salesRepId = salesRepId;
					note.salesRepLatitude = inputItem.salesRepLatitude;
					note.salesRepLongitude = inputItem.salesRepLongitude;
					note.firstName = inputItem.firstName;
					note.lastName = inputItem.lastName;
					note.categoryId = inputItem.categoryId;
					note.systemId = inputItem.systemId;
					note.note = inputItem.note;
				}

				SalesContactAddress address = null;
				if (!string.IsNullOrEmpty(inputItem.address) ||
					!string.IsNullOrEmpty(inputItem.city) ||
					!string.IsNullOrEmpty(inputItem.state) ||
					!string.IsNullOrEmpty(inputItem.zip))
				{
					address = new SalesContactAddress();
					address.address = inputItem.address;
					//address.address2 = inputItem.address2;
					address.state = inputItem.state;
					address.city = inputItem.city;
					address.zip = inputItem.zip;
				}

				SalesContactFollowup followup = null;
				if (inputItem.followup.HasValue)
				{
					followup = new SalesContactFollowup();
					followup.salesRepId = salesRepId;
					followup.followupTimestamp = inputItem.followup.Value;
				}

				return await Srv.SaveContactAndInfoAsync(contact, note, address, followup);
			};

			//$http.get('ng/Contact/get_contacts_in_area/salesRepId=' + sr_id + '&officeId=' + o_id + '&minlat=' + sw.lat() + '&maxlat=' + ne.lat() + '&minlng=' + sw.lng() + '&maxlng=' + ne.lng()
			//Get["/get_contacts_in_area/{splat*}", true] = async (x, ct) =>
			//{
			//	string splat = x.splat;
			//	var ray = splat.Split(new char[] { '&' });
			//
			//	int salesRepId = 0, officeId = 0;
			//	double minlat = 0, minlng = 0, maxlat = 0, maxlng = 0;
			//	foreach (var txt in ray)
			//	{
			//		var kvp = txt.Split(new char[] { '=' });
			//		var v = kvp[1];
			//		if (string.Equals("null", v, StringComparison.OrdinalIgnoreCase))
			//			v = "0";
			//		switch (kvp[0])
			//		{
			//			case "salesRepId":
			//				salesRepId = int.Parse(v);
			//				break;
			//			case "officeId":
			//				officeId = int.Parse(v);
			//				break;
			//			case "minlat":
			//				minlat = double.Parse(v);
			//				break;
			//			case "minlng":
			//				minlng = double.Parse(v);
			//				break;
			//			case "maxlat":
			//				maxlat = double.Parse(v);
			//				break;
			//			case "maxlng":
			//				maxlng = double.Parse(v);
			//				break;
			//		}
			//	}
			//	return await Srv.ContactsInAreaAsync(salesRepId, officeId, minlat, minlng, maxlat, maxlng);
			//};
			Get["/InBounds", true] = async (x, ct) =>
			{
				var query = this.Bind<ContactsQuery>();
				return await Srv.ContactsInAreaAsync(query.salesRepId, query.officeId, query.minlat, query.minlng, query.maxlat, query.maxlng);
			};

			//$http.get('ng/Contact/get_contacts_by_hour/salesRepId=' + $scope.salesRepId + '&officeId=' + $scope.officeId + '&startTimestamp=' + this.startTimestamp.formatTimestamp() + '&endTimestamp=' + this.endTimestamp.formatTimestamp() )
			Get["/get_contacts_by_hour", true] = async (x, ct) =>
			{
				string splat = x.splat;
				var ray = splat.Split(new char[] { '&' });

				int salesRepId = 0, officeId = 0;
				DateTime startTimestamp = DateTime.Now, endTimestamp = DateTime.Now;
				foreach (var txt in ray)
				{
					var kvp = txt.Split(new char[] { '=' });
					var v = kvp[1];
					switch (kvp[0])
					{
						case "salesRepId":
							salesRepId = int.Parse(v);
							break;
						case "officeId":
							officeId = int.Parse(v);
							break;
						case "startTimestamp":
							startTimestamp = DateTime.Parse(v);
							break;
						case "endTimestamp":
							endTimestamp = DateTime.Parse(v);
							break;
					}
				}
				return await Srv.ContactsByHourAsync(salesRepId, officeId, startTimestamp, endTimestamp);
			};
		}
	}

	class ContactsQuery
	{
		public int salesRepId { get; set; }
		public int officeId { get; set; }
		public double minlat { get; set; }
		public double minlng { get; set; }
		public double maxlat { get; set; }
		public double maxlng { get; set; }
	}
}
