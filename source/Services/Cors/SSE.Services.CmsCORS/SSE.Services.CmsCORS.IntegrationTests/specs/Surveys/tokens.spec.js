var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var sessionId;
var authBody;

describe("Token test", function() {
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
  it("Read all, SurveySrv/tokens/.", function(done) {
    // ** Get all tokens.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/tokens/",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      done();
    });
  });

  it("Read one. SurveySrv/tokens/1", function(done) {
    // ** Get a token.
    request.get({
        url: config.SseServicesCmsCORS + "SurveySrv/tokens/1",
        json: true
      },
      function(error, response, body) {
        expect(error).toBeNull();
        expect(body).not.toBeNull();
        expect(body.Code).toBe(0);
        expect(body.Value).not.toBeUndefined();

        //console.log("Token: ", body.Value);
        done();
      });

  });
});