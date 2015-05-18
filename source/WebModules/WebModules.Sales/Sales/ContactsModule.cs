using Nancy.Authentication.Token;
using NXS.Lib.Web;
using NXS.Lib.Web.Authentication;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules.Sales.Sales
{
	public class ContactsModule : BaseModule
	{
		public ContactsModule(			)
			: base("/Auth/User")
		{

		}

	}
}
