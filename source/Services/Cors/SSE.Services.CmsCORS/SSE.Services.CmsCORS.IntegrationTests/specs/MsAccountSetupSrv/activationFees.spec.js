/** ActivationFees Spec. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
//var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("ActivationFee.spec tests | ", function() {
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

  it("Get ActivationFee List", function(done) {
    // Execute
    request.get({
      url: config.SseServicesCmsCORS + "MsAccountSetupSrv/ActivationFees",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No ActivationFees were returned.");

      if (body.Value.length > 0) {
        expect(body.Value[0].ItemID).not.toBeNull();
        // console.log("Activation Fees: ", body.Value);
      }

      done();
    });
  });
});