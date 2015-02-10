/** Point Systems. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
//var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("PointSystem.spec tests | ", function() {
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

  it("Get Point Systems List", function(done) {
    // Execute
    request.get({
      url: config.SseServicesCmsCORS + "MsAccountSetupSrv/PointSystems",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No PointSystems were returned.");
      //console.log("Body:", body);

      if (body.Value.length > 0) {
        expect(body.Value[0].InvoiceTemplateID).not.toBeNull();
      }

      done();
    });
  });
});