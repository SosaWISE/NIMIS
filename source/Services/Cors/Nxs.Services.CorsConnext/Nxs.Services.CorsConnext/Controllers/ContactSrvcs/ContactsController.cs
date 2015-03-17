using System.Collections.Generic;
using System.Web.Http;

namespace Nxs.Services.CorsConnext.Controllers.ContactSrvcs
{
	[RoutePrefix("ContactSrvcs")]
	public class ContactsController : ApiController
    {
        // GET api/contacts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/contacts/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/contacts
        public void Post([FromBody]string value)
        {
        }

        // PUT api/contacts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/contacts/5
        public void Delete(int id)
        {
        }
    }
}
