/** CustomerCardInfo.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("CustomerCardInfo.spec tests | ", function() {
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
  it("Calling a customer with his CMFID", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "AccountingEngineSrv/CustomerCardInfos/" + 100154,
      // url: config.SseServicesCmsCORS + "AccountingEngineSrv/CustomerCardInfos/" + 3050456,
      json: true
    }, function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      modelDef.aeCustomerCardInfo(body.Value);
      done();
    });
  });

  /** 
  These are brittle tests */
  it("Calling a customer with his AccountID", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "AccountingEngineSrv/CustomerCardInfos/" + 151148 + "/Customer",
      // url: config.SseServicesCmsCORS + "AccountingEngineSrv/CustomerCardInfos/" + 3050456,
      json: true
    }, function(error, response, body) {
      console.log("Body of CustomerInfo by AccountId: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      modelDef.aeCustomerCardInfo(body.Value);
      done();
    });
  });

});