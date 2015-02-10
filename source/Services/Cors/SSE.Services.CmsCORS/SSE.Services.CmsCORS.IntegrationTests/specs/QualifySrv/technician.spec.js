/** technician.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
//var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("Technician.spec tests | ", function() {
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

  it("Get Technician Info", function(done) {
    // Execute
    request.get({
      url: config.SseServicesCmsCORS + "QualifySrv/Technician/SYST001",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No Rep was returned.");

      /** Check all properties returned. */
      expect(body.Value.UserID).toBeGreaterThan(0, "UserID is required and has to be greater than zero.");
      expect(body.Value.CompanyID).not.toBeNull("Company ID is required.");
      expect(body.Value.FirstName).not.toBeNull("FirstName is required.");
      expect(body.Value.LastName).not.toBeNull("LastName is required.");
      expect(body.Value.CompanyName).not.toBeNull("CompanyName is required.");
      expect(body.Value.UserName).not.toBeNull("UserName is required.");
      expect(body.Value.BirthDate).not.toBeNull("BirthDate is required.");
      expect(body.Value.HomeTown).not.toBeNull("HomeTown is required.");
      expect(body.Value.Sex).not.toBeNull("Sex is required.");
      expect(body.Value.ShirtSize).not.toBeNull("Shirt is required.");
      expect(body.Value.HatSize).not.toBeNull("HatSize is required.");
      expect(body.Value.PhoneHome).not.toBeNull("PhoneHome is required.");
      expect(body.Value.PhoneCell).not.toBeNull("PhoneCell is required.");
      expect(body.Value.Email).not.toBeNull("Email is required.");
      expect(body.Value.SSN).not.toBeNull("SSN is required.");
      expect(body.Value.ImagePath).not.toBeNull("Image is required");
      if (body.Value.Seasons !== null) {
        expect(body.Value.Seasons.length).toBeGreaterThan(0, "Every tech should have a season associated with it.");
      }

      // console.log("Seasons: ", body.Value);

      done();
    });
  });

  it("Save Technician Info", function(done) {
    // Execute
    request.post({
      url: config.SseServicesCmsCORS + "QualifySrv/Technician",
      form: {
        MsAccountId: 151148,
        CompanyId: 'SYST001'
      },
      json: true
    }, function(error, response, body) {
      // console.log("Save TechInfo Body: ", body);
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No Rep was returned.");

      /** Check all properties returned. */
      expect(body.Value.UserID).toBeGreaterThan(0, "UserID is required and has to be greater than zero.");
      expect(body.Value.CompanyID).not.toBeNull("Company ID is required.");
      expect(body.Value.FirstName).not.toBeNull("FirstName is required.");
      expect(body.Value.LastName).not.toBeNull("LastName is required.");
      expect(body.Value.CompanyName).not.toBeNull("CompanyName is required.");
      expect(body.Value.UserName).not.toBeNull("UserName is required.");
      expect(body.Value.BirthDate).not.toBeNull("BirthDate is required.");
      expect(body.Value.HomeTown).not.toBeNull("HomeTown is required.");
      expect(body.Value.Sex).not.toBeNull("Sex is required.");
      expect(body.Value.ShirtSize).not.toBeNull("Shirt is required.");
      expect(body.Value.HatSize).not.toBeNull("HatSize is required.");
      expect(body.Value.PhoneHome).not.toBeNull("PhoneHome is required.");
      expect(body.Value.PhoneCell).not.toBeNull("PhoneCell is required.");
      expect(body.Value.Email).not.toBeNull("Email is required.");
      expect(body.Value.SSN).not.toBeNull("SSN is required.");
      expect(body.Value.ImagePath).not.toBeNull("Image is required");
      if (body.Value.Seasons !== null) {
        expect(body.Value.Seasons.length).toBeGreaterThan(0, "Every tech should have a season associated with it.");
      }

      // console.log("Seasons: ", body.Value);

      done();
    });
  });

});