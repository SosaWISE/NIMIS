/** billingInfoSummary.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var sessionId;
var authBody;

describe("BillingInfoSummary.spec tests | ", function() {
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

  /** 
	These are brittle tests */
  it("Get billing info summary by CMFID", function(done) {
    request.get({
      //url: config.SseServicesCmsCORS + "AccountingEngineSrv/BillingInfoSummary/3000042/CMFID",
      url: config.SseServicesCmsCORS + "AccountingEngineSrv/BillingInfoSummary/3000001/CMFID",
      json: true
    }, function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      done();
    });
  });

  it("Get billing info summary by AccountId", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "AccountingEngineSrv/BillingInfoSummary/100166/AccountId",
      json: true
    }, function(error, response, body) {
      //console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      done();
    });
  });

});