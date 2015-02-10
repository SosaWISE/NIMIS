/** Localizations.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
//var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("localizations.spec tests | ", function() {
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

  it("Get all Localizations.", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "MainCoreSrv/Localizations",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No values returned.");

      // console.log("Localizations Body: ", body);

      /** Check data elements. */
      expect(body.Value.length).toBeDefined("This is an array of values so there must be a length");
      if (body.Value.length > 0) {
        modelDef.localization(body.Value[0]);
      }

      done();

    });
  });
});