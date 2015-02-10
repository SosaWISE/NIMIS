/** DoNotCall.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
// var modelDef = require("../modelDefinitions");
// var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("DoNotCall.spec tests. | ", function() {
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody).toBeDefined("AuthBody is not defined.");
      expect(authBody.Code).toBe(0);
      done();
    });
  });
  // **   END BEFORE EACH.

  /** Generate Barcode. */
  it("Validate DoNotCall Found Test", function(done) {
    request.get({
        url: config.SseServicesCmsCORS + "DoNotCallSrv/PhoneNumbers/3850007359",
        json: true
      },
      function(error, response, body) {
        console.log("Body for DNC: ", body);
        expect(error).toBeNull();
        expect(body).not.toBeNull();
        expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);

        done();
      });
  });
});