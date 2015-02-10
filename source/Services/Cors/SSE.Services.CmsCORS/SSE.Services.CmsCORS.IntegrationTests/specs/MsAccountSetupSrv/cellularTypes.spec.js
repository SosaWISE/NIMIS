/** cellularTypes.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
//var dataSource = require("../dataSource");
var modelDef = require("../modelDefinitions.js");
var sessionId;
var authBody;

describe("CellularTypes.spec tests | ", function() {
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

  it("Get CellularTypes List", function(done) {
    // Execute
    request.get({
      url: config.SseServicesCmsCORS + "MsAccountSetupSrv/CellularTypes",
      json: true
    }, function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No ActivationFees were returned.");

      if (body.Value.length > 0) {
        expect(body.Value[0].CellularTypeID).not.toBeNull();
        // console.log("Cellular Types: ", body.Value);

        var items = body.Value;
        items.map(function(item) {
          modelDef.msCellularTypes(item);
        });
      }

      done();
    });
  });
});