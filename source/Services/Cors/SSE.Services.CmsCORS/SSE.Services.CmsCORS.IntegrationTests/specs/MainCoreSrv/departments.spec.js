/** departments.spec.js. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var sessionId;
var authBody;

describe("Departments.spec tests | ", function() {
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

  it("Get all departments.", function(done) {
    // ** Get all tokens.
    request.get({
      url: config.SseServicesCmsCORS + "MainCoreSrv/Departments",
      json: true
    }, function(error, response, body) {
      //console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      done();
    });
  });
});