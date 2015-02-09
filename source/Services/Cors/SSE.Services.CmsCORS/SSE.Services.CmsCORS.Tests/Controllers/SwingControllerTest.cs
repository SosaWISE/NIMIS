/**
 * reagan/junryll
 this is the unit test for Swing Controller
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOS.FunctionalServices.Contracts.Helper;
using SSE.Services.CmsCORS.Controllers.Swing;
using SOS.Services.Interfaces.Models.Swing;
using SSE.Services.CmsCORS.Models;
using SSE.Services.CmsCORS.Tests.Helpers;


namespace SSE.Services.CmsCORS.Tests.Controllers
{
    [TestClass]
    public class SwingControllerTest
    {
        [TestMethod]
        public void CustomerSwingInfoRead()
        {
            var swingCntlr = new SwingController();

            CmsCORSResult<CustomerSwingInfo> result = swingCntlr.CustomerSwingInfoRead(100000, "PRI");

            // ** Check my result
            Assert.IsTrue(result.Code == (int)ErrorCodes.Success, "There was an error running CustomerSwingInfoRead");


        }


        [TestMethod]
        public void CustomerSwingPremiseAddressRead()
        {
            var swingCntlr = new SwingController();

            CmsCORSResult<CustomerSwingPremiseAddress> result = swingCntlr.CustomerSwingPremiseAddressRead(100000);
            // ** Check my result
            Assert.IsTrue(result.Code == (int)ErrorCodes.Success, "There was an error running CustomerSwingPremiseAddress");

        }

        [TestMethod]
        public void CustomerSwingEmergencyContactRead()
        {
            var swingCntlr = new SwingController();

            CmsCORSResult<CustomerSwingEmergencyContact> result = swingCntlr.CustomerSwingEmergencyContactRead(100000);
            // ** Check my result
            Assert.IsTrue(result.Code == (int)ErrorCodes.Success, "There was an error running CustomerSwingPremiseAddress");

        }

        [TestMethod]
        public void CustomerSwingEquipmentInfoRead()
        {
            var swingCntlr = new SwingController();

            CmsCORSResult<CustomerSwingEquipmentInfo> result = swingCntlr.CustomerSwingEquipmentInfoRead(100000);
            // ** Check my result
            Assert.IsTrue(result.Code == (int)ErrorCodes.Success, "There was an error running CustomerSwingPremiseAddress");

        }
    }
}
