/** Test empty list. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var sessionId;
var authBody;

describe("EmptyListTest.spec tests | ", function() {
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

  it("Read all Surveys EmptyList.", function(done) {
    // console.log("Making the call...");
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/Surveys",
      json: true
    }, function(error, response, body) {
      // console.log("Body for the call " + config.SseServicesCmsCORS + "SurveySrv/Surveys: ", body);
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(Array.isArray(body.Value)).toBe(true, "This should return an array.");

      done();
    });
  });
});