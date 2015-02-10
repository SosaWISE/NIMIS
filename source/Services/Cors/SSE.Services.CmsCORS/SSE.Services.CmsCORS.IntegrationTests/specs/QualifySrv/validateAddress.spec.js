/** Verify Address. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var dataSource = require("../dataSource");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("ValidateAddress.spec tests. | ", function() {
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

  it("Validate an address |", function(done) {
    // Execute
    request.post({
      url: config.SseServicesCmsCORS + "QualifySrv/AddressValidation",
      form: {
        DealerId: config.DealerId, // Required
        SeasonId: config.SeasonId, // Required
        TeamLocationId: config.TeamLocationId, // Required
        SalesRepId: config.SalesRepId, // Required
        StreetAddress: "1184 N 840 E",
        City: "Orem",
        StateId: "UT",
        PostalCode: "84097",
        PhoneNumber: "8012267067"
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);
      console.log("Body: ", body);

      done();
    });
  });

  it("Read an address |", function(done) {
    // ** Create an address
    dataSource.verifyAddress(request, {
      DealerId: config.DealerId,
      SeasonId: config.SeasonId,
      TeamLocationId: config.TeamLocationId,
      SalesRepId: config.SalesRepId,
      StreetAddress: "1184 N 840 E",
      City: "Orem",
      StateId: "UT",
      PostalCode: "84097",
      PhoneNumber: "8012267067"
    }, function(address) {
      expect(address).not.toBeNull("Address verification failed.");
      // ** Make read call.
      request.get({
        url: config.SseServicesCmsCORS + "QualifySrv/AddressValidation/" + address.AddressID,
        json: true
      }, function(error, response, body) {
        // console.log("Finish body:", body);
        expect(error).toBeNull("There was an error", error);
        expect(body).not.toBeNull("There was no body.");
        expect(body.Code).toBe(0, "There was an error in the body message", body);
        expect(body.Value.AddressID).toBe(address.AddressID);
      });
      // ** Finish
      done();
    });
  });

  it("Address with no post directional |", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "QualifySrv/AddressValidation",
      form: {
        DealerId: config.DealerId, // Required
        SeasonId: config.SeasonId, // Required
        TeamLocationId: config.TeamLocationId, // Required
        SalesRepId: config.SalesRepId, // Required
        StreetAddress: "1573 N Technology Way",
        City: "OREM",
        StateId: "UT",
        PostalCode: "84097",
        PhoneNumber: "(801) 822-1234"
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);
      // console.log("Body: ", body);


      done();
    });
  });
  it("Address not found | ", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "QualifySrv/AddressValidation",
      form: {
        DealerId: config.DealerId, // Required
        SeasonId: config.SeasonId, // Required
        TeamLocationId: config.TeamLocationId, // Required
        SalesRepId: config.SalesRepId, // Required
        StreetAddress: "11115 S 1700 E",
        // City: "SANDY",
        // StateId: "UT",
        PostalCode: "84096", // Wrong Zip code
        PhoneNumber: "(801) 822-1234"
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(60100, "The wrong code was returned.  Message: " + body.Message);
      // console.log("Body: ", body);

      done();
    });
  });

  it("Address found with minimal info | ", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "QualifySrv/AddressValidation",
      form: {
        DealerId: config.DealerId, // Required
        SeasonId: config.SeasonId, // Required
        TeamLocationId: config.TeamLocationId, // Required
        SalesRepId: config.SalesRepId, // Required
        StreetAddress: "11115 S 1700 E",
        // City: "SANDY",
        // StateId: "UT",
        PostalCode: "84092", // Required
        PhoneNumber: "(801) 822-1234"
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "There was an error with the following message: " + body.Message);
      // console.log("Body: ", body);

      /** Check dataModel. */
      modelDef.address(body.Value);

      done();
    });
  });
});