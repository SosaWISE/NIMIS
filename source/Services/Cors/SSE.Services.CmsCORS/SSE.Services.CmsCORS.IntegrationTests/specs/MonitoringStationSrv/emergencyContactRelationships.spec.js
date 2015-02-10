/** EmergencyContactRelationships.spec.js. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("EmergencyContactRelationships.spec tests | ", function() {
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
  it("Get all EMC relationships", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "MsAccountSetupSrv/EmergencyContactRelationships",
      json: true
    }, function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0, "Code did not return a successfull code.");
      expect(body.Value.length).toBeGreaterThan(0, "This should have returned a list of one.");

      /** Data fields validation. */
      body.Value.map(function(item) {
        modelDef.msEmergencyContactRelationship(item);
      });
      done();
    });
  });
});