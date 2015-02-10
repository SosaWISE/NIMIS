/** EquipmentLocation.spec.js. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("EquipmentLocation.spec tests. | ", function() {
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

  it("Validate getting Equipment Locations", function(done) {
    request.get({
        url: config.SseServicesCmsCORS + "MsAccountSetupSrv/MonitoringStationOS/MI_DICE/EquipmentLocations",
        json: true
      },
      function(error, response, body) {
        // console.log("Body: ", body);
        expect(error).toBeNull();
        expect(body).not.toBeNull();
        expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);

        /** Check for data elements. */
        expect(body.Value.length).toBeGreaterThan(0, "The value should be a list of items.");
        body.Value.map(function(item) {
          modelDef.equipmentLocation(item);
        });
        done();
      });
  });
});