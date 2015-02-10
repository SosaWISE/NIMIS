/** CustomerSearch.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("CustomerSearch.spec tests | ", function() {
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

  /** 
	These are brittle tests */
  it("Search by using all Fields", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "AccountingEngineSrv/CustomerSearches",
      form: {
        FirstName: 'AAAAAAAAA*', // Can use wild cards.
        LastName: 'SDDDDDDDDDDDDDDDD*', // Can use wild cards.
        City: '0000000000',
        StateId: 'ZZ',
        PostalCode: '00000',
        PhoneNumber: '0000000000',
        PageSize: 30,
        PageNumber: 1 // Page number must start with 1.  Zero will not work.
      },
      json: true
    }, function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      done();
    });
  });

  it("Search by using no fields", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "AccountingEngineSrv/CustomerSearches",
      form: {},
      json: true
    }, function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(1100, "A validation error should have been returned.");

      done();
    });
  });

  it("Search and get values back.", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "AccountingEngineSrv/CustomerSearches",
      form: {
        FirstName: 'b*', // Can use wild cards.
        PageSize: 30,
        PageNumber: 1 // Page number must start with 1.  Zero will not work.
      },
      json: true
    }, function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      /** Check the length. */
      expect(body.Value.length).toBe(30, "The length of the result should be 30, but instead it was : '" + body.Value.length + "'.");

      // Loop through each item and check that it is working
      var itemsList = body.Value;
      itemsList.map(function(item) {
        modelDef.aeCustomerGeneralSearchDetails(item);
      });

      done();
    });
  });

});