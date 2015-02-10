/** frequentlyInstalledEquipment.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
//var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("frequentlyInstalledEquipment.spec tests | ", function() {
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

  it("Get frequentlyInstalledEquipment List", function(done) {
    // Execute
    request.get({
      url: config.SseServicesCmsCORS + "MsAccountSetupSrv/FrequentlyInstalledEquipmentGet",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No FrequentlyInstalledEquipmentGet were returned.");

      if (body.Value.length > 0) {
        // ** Check for each field that is populated.
        expect(body.Value[0].ItemID).not.toBeNull();
        expect(body.Value[0].ItemTypeId).not.toBeNull();
        expect(body.Value[0].TaxOptionId).not.toBeNull();
        expect(body.Value[0].ItemFKID).not.toBeUndefined();
        expect(body.Value[0].ItemSKU).not.toBeUndefined();
        expect(body.Value[0].ItemDesc).not.toBeNull();
        expect(body.Value[0].Price).not.toBeNull();
        expect(body.Value[0].Cost).not.toBeNull();
        expect(body.Value[0].SystemPoints).not.toBeNull();
        expect(body.Value[0].IsCatalogItem).not.toBeNull();
        expect(body.Value[0].IsActive).not.toBeNull();
        expect(body.Value[0].IsDeleted).not.toBeNull();
        // console.log("FrequentlyInstalledEquipmentGet: ", body.Value);
      }

      done();
    });
  });
});