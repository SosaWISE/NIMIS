/** Verify Address tests. */
/** Sales Rep tests */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
//var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("VerifyAddress.spec tests | ", function() {
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
  it("Verify an address", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "QualifySrv/VerifyAddress",
      form: {
        DealerId: config.DealerId,
        SeasonId: config.SeasonId,
        TeamLocationId: config.TeamLocationId,
        SalesRepId: config.SalesRepId,
        StreetAddress: "1184 N 840 E",
        City: "Orem",
        StateId: "UT",
        PostalCode: "84097",
        PhoneNumber: "8019220987"
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No Rep was returned.");
      expect(body.Value.Validated).toBe(true, "This address was expected to validate.");
      //console.log("Body: ", body);

      done();
    });
  });
});