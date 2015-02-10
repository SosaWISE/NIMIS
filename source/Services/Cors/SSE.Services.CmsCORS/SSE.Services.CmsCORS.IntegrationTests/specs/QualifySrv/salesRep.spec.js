/** Sales Rep tests */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
//var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("SalesRep.spec tests | ", function() {
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

  it("Get Sales Rep Info", function(done) {
    // Execute
    request.get({
      url: config.SseServicesCmsCORS + "QualifySrv/SalesRep/SOSA001",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No Rep was returned.");

      // console.log("Sales Rep Body: ", body);
      // console.log("Seasons: ", body.Value.Seasons);

      /** Check data elements. */
      modelDef.salesRepAndTechInfo(body.Value);

      done();
    });
  });
});