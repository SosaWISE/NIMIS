/* QualifyCustomerInfo.spec.js. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var dataSource = require("../dataSource");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("QualifyCustomerInfo.spec tests. | ", function() {
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

  it("Create a new Lead.", function(done) {
    dataSource.verifyAddress(request, {
      DealerId: 5001,
      SeasonId: 1,
      TeamLocationId: 1,
      SalesRepId: 'SOSA001',
      StreetAddress: '1128 E 3300 S',
      City: 'Salt Lake City',
      StateId: 'UT',
      PostalCode: '84106',
      PhoneNumber: '(801) 486-8251'

    }, function(addrValue) {
      // ** Run Credit
      request.post({
        url: config.SseServicesCmsCORS + "QualifySrv/runCredit",
        form: {
          AddressId: addrValue.AddressID,
          DealerId: 5000, // *REQUIRED:  This is which dealer is getting credit for the lead.  5000: Is NexSense.
          TeamLocationId: 1, // *REQUIRED:  This is the office that credit is being ran from.  1: Would be default which means corporate.  For summer sales we would need to pass the correct office.
          SeasonId: 1, // *REQUIRED:  This is the season that it was ran.  1: Would be the default for year round.
          SalesRepId: 'SOSA001', // *REQUIRED:  This is the Company ID of a rep.
          LocalizationId: 'en-us', // *REQUIRED:  This is the default language that they speak.
          LeadSourceId: '7', // *REQUIRED:  This is the web.
          LeadDispositionId: 9, // *REQUIRED:  This is in what state is the lead.  9: Means "Ran Credit"
          Salutation: 'Mr.', // *OPTIONAL
          FirstName: 'Kirby', // *REQUIRED
          MiddleName: null, // *OPTIONAL
          LastName: 'Vacuum', // *REQUIRED
          Suffix: 'II', // *OPTIONAL
          Gender: 'Male', // *REQUIORED
          SSN: '333-33-3333', // *CONDITIONAL:  Either SSN or DOB has to be present or both.
          DOB: '12/14/1968', // *CONDITIONAL:  Either SSN or DOB has to be present or both.
          Dl: '2342423423', // *OPTIONAL
          DlStateId: 'UT', // *OPTIONAL
          Email: 'sammy@sam.com', // *REQUIORED
          PhoneHome: '(801) 654-9877', // *OPTIONAL
          PhoneWork: null, // *OPTIONAL
          PhoneMobile: '(909) 987-6790' // *OPTIONAL
        },
        json: true
      }, function(error, response, body) {
        expect(error).toBeNull();
        expect(body).not.toBeNull();
        expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
        expect(body.Value).not.toBeNull("No Rep was returned.");
        // console.log("Lead Body: ", body);

        /** Check data elements. */
        modelDef.lead(body.Value);

        request.get({
          url: config.SseServicesCmsCORS + "QualifySrv/QualifyCustomerInfos/" + body.Value.LeadId + "/Lead/",
          json: true
        }, function(error, response, body) {
          expect(error).toBeNull();
          expect(body).not.toBeNull();
          expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
          // console.log("Qualify Customer Info Body: ", body);
          modelDef.qlQualifyCustomerInfo(body.Value);

          /** Create an account from this lead and then search it by it's customer id. */
          dataSource.createMsAccount(request, {
            leadId: body.Value.LeadID
          }, function(respValue) {
            // console.log("Create MS Account Response: ", respValue);
            request.get({
              url: config.SseServicesCmsCORS + "QualifySrv/QualifyCustomerInfos/" + respValue.CustomerId + "/Customer/",
              json: true
            }, function(error, response, body) {
              expect(error).toBeNull();
              expect(body).not.toBeNull();
              expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
              // console.log("Customer Info Body: ", body);

              /** Check data elements. */
              modelDef.qlQualifyCustomerInfo(body.Value);

              request.get({
                url: config.SseServicesCmsCORS + "QualifySrv/QualifyCustomerInfos/" + respValue.AccountID + "/Account/",
                json: true
              }, function(error, response, body) {
                expect(error).toBeNull();
                expect(body).not.toBeNull();
                expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
                // console.log("Customer Info Body by AccountId: ", body);

                /** Check data elements. */
                modelDef.qlQualifyCustomerInfo(body.Value);

                done();
              });
            });
          });
        });
      });

    });

  });

});