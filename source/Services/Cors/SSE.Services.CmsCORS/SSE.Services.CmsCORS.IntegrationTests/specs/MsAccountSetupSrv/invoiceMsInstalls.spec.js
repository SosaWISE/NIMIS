/** invoiceMsInstalls.spec.js. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var sessionId;
var authBody;

describe("InvoiceMsInstalls.spec tests | ", function() {
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody.Code).toBe(0);
      done();
    });
  });
  // **   END BEFORE EACH.

  /** Supporting functions. */
  function checkData(item) {
    expect(item.InvoiceID).toBeDefined("InvoiceID must be defined.");
    expect(item.InvoiceID).toBeGreaterThan(0, "InvoiceID must have a value greater than 0.");
    expect(item.AccountId).toBeDefined("AccountId must be defined.");
    expect(item.AccountId).toBeGreaterThan(0, "AccountId must have a value greater than 0.");
    expect(item.ActivationFeeItemId).toBeDefined("ActivationFeeItemId must be defined.");
    expect(item.ActivationFeeItemId.length).toBeGreaterThan(0, "ActivationFeeItemId must have a value and ID.");
    expect(item.ActivationFeeActual).toBeDefined("ActivationFeeActual must be defined.");
    expect(item.ActivationFeeActual).toBeGreaterThan(-1, "ActivationFeeActual must have a value greater than 0.");
    expect(item.ActivationFee).toBeDefined("ActivationFee must be defined.");
    expect(item.ActivationFee).toBeGreaterThan(0, "ActivationFee must have a value greater than 0.");
    expect(item.MonthlyMonitoringRateItemId).toBeDefined("MonthlyMonitoringRateItemId must be defined.");
    expect(item.MonthlyMonitoringRateItemId.length).toBeGreaterThan(0, "MonthlyMonitoringRateItemId must have a value and ID.");
    expect(item.MonthlyMonitoringRateActual).toBeDefined("MonthlyMonitoringRateActual must be defined.");
    expect(item.MonthlyMonitoringRateActual).toBeGreaterThan(-1, "MonthlyMonitoringRateActual must have a value greater than 0.");
    expect(item.MonthlyMonitoringRate).toBeDefined("MonthlyMonitoringRate must be defined.");
    expect(item.MonthlyMonitoringRate).toBeGreaterThan(0, "MonthlyMonitoringRate must have a value greater than 0.");

  }
  /** These are brittle tests. */
  it("Get InvoiceMsInstalls by AccountId", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "MsAccountSetupSrv/InvoiceMsIsntalls/100212/AccountId",
      json: true
    }, function(error, response, body) {
      // console.log("Body by accountid: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);
      expect(body.Value).not.toBeNull("The method should have returned an object.");

      /** Check on data. */
      var item = body.Value,
        invoiceID = item.InvoiceID;
      checkData(item);

      /** Get by InvoiceID. */
      request.get({
        url: config.SseServicesCmsCORS + "MsAccountSetupSrv/InvoiceMsIsntalls/" + invoiceID + "/InvoiceID",
        json: true
      }, function(error, response, body) {
        // console.log("Body by InvoiceID: ", body);
        expect(error).toBeNull();
        expect(body.Code).toBe(0);
        expect(body.Value).not.toBeNull("The method should have returned an object.");

        /** Check on data. */
        var item = body.Value;
        checkData(item);

        done();

      });
    });

  });
});