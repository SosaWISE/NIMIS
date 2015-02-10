/** Address.spec.js. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
// var dataSource = require("../dataSource");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("Address.spec tests. | ", function() {
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

  it("Create an address |", function(done) {
    // Execute
    request.post({
      url: config.SseServicesCmsCORS + "QualifySrv/AddressValidation",
      form: {
        DealerId: config.DealerId, // Required
        SeasonId: config.SeasonId, // Required
        TeamLocationId: config.TeamLocationId, // Required
        SalesRepId: config.SalesRepId, // Required
        StreetAddress: "722 E Technology Ave",
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
      // console.log("Body: ", body);

      var validAddr = body.Value;
      /*
       * GET
       */
      // ** Read full address
      request.get({
        url: config.SseServicesCmsCORS + "QualifySrv/Addresses/" + validAddr.AddressID,
        json: true
      }, function(error, response, body) {
        expect(error).toBeNull();
        expect(body).not.toBeNull();
        expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);
        // console.log("Get Address Body: ", body);

        validAddr = body.Value;

        /*
         * POST
         */
        /** Create a new address with the Create method. */
        request.post({
          url: config.SseServicesCmsCORS + "QualifySrv/Addresses",
          form: {
            DealerId: validAddr.DealerId,
            AddressTypeId: validAddr.AddressTypeId,
            AddressValidationStateId: validAddr.AddressValidationStateId,
            CarrierRoute: validAddr.CarrierRoute,
            City: 'American Fork',
            CongressionalDistric: validAddr.CongressionalDistric,
            CountryId: validAddr.CountryId,
            County: validAddr.County,
            CountyCode: validAddr.CountyCode,
            DeliveryPoint: validAddr.DeliveryPoint,
            DPV: validAddr.DPV,
            DPVFootnote: validAddr.DPVFootnote,
            DPVResponse: validAddr.DPVResponse,
            Extension: validAddr.Extension,
            ExtensionNumber: validAddr.ExtensionNumber,
            Latitude: validAddr.Latitude,
            Longitude: validAddr.Longitude,
            Phone: validAddr.Phone,
            PlusFour: validAddr.PlusFour,
            PostalCode: validAddr.PostalCode,
            PostalCodeFull: validAddr.PostalCodeFull,
            PostDirectional: validAddr.PostDirectional,
            PreDirectional: validAddr.PreDirectional,
            SalesRepId: validAddr.SalesRepId,
            SeasonId: validAddr.SeasonId,
            StateId: validAddr.StateId,
            StreetAddress: validAddr.StreetAddress,
            StreetAddress2: 'Suite 1100; Bldg E',
            StreetName: validAddr.StreetName,
            StreetNumber: validAddr.StreetNumber,
            StreetType: validAddr.StreetType,
            TeamLocationId: validAddr.TeamLocationId,
            TimeZoneId: validAddr.TimeZoneId,
            Urbanization: validAddr.Urbanization,
            UrbanizationCode: validAddr.UrbanizationCode,
            ValidationVendorId: validAddr.ValidationVendorId
          },
          json: true
        }, function(error, response, body) {
          expect(error).toBeNull();
          expect(body).not.toBeNull();
          expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);
          expect(body.Value.City).toBe("American Fork", "City did not change.");
          expect(body.Value.StreetAddress2).toBe("Suite 1100; Bldg E", "StreetAddress2 did not change.");
          // console.log("Body Create Address: ", body);

          modelDef.qlAddress(body.Value);

          validAddr = body.Value;

          /*
           * PUT
           */
          request.post({
            url: config.SseServicesCmsCORS + "QualifySrv/Addresses/" + validAddr.AddressID,
            form: {
              AddressID: validAddr.AddressID,
              DealerId: validAddr.DealerId,
              AddressTypeId: validAddr.AddressTypeId,
              AddressValidationStateId: validAddr.AddressValidationStateId,
              CarrierRoute: validAddr.CarrierRoute,
              City: 'OREM',
              CongressionalDistric: validAddr.CongressionalDistric,
              CountryId: validAddr.CountryId,
              County: validAddr.County,
              CountyCode: validAddr.CountyCode,
              DeliveryPoint: validAddr.DeliveryPoint,
              DPV: validAddr.DPV,
              DPVFootnote: validAddr.DPVFootnote,
              DPVResponse: validAddr.DPVResponse,
              Extension: validAddr.Extension,
              ExtensionNumber: validAddr.ExtensionNumber,
              Latitude: validAddr.Latitude,
              Longitude: validAddr.Longitude,
              Phone: validAddr.Phone,
              PlusFour: validAddr.PlusFour,
              PostalCode: validAddr.PostalCode,
              PostalCodeFull: validAddr.PostalCodeFull,
              PostDirectional: validAddr.PostDirectional,
              PreDirectional: validAddr.PreDirectional,
              SalesRepId: validAddr.SalesRepId,
              SeasonId: validAddr.SeasonId,
              StateId: validAddr.StateId,
              StreetAddress: validAddr.StreetAddress,
              StreetAddress2: null,
              StreetName: validAddr.StreetName,
              StreetNumber: validAddr.StreetNumber,
              StreetType: validAddr.StreetType,
              TeamLocationId: validAddr.TeamLocationId,
              TimeZoneId: validAddr.TimeZoneId,
              Urbanization: validAddr.Urbanization,
              UrbanizationCode: validAddr.UrbanizationCode,
              ValidationVendorId: validAddr.ValidationVendorId
            },
            json: true
          }, function(error, response, body) {
            expect(error).toBeNull();
            expect(body).not.toBeNull();
            expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);
            expect(body.Value.City).toBe("OREM", "City did not change.");
            // console.log("Body Create Address: ", body);

            modelDef.qlAddress(body.Value);

            /*
             * DELETE
             */
            request.del({
              url: config.SseServicesCmsCORS + "QualifySrv/Addresses/" + validAddr.AddressID,
              json: true
            }, function(error, response, body) {
              expect(error).toBeNull();
              expect(body).not.toBeNull();
              expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);
              // console.log("Body Delete Address: ", body);

              expect(body.Value).toBe(true, "There was an error trying to delete the address.");

              request.get({
                url: config.SseServicesCmsCORS + "QualifySrv/Addresses/" + validAddr.AddressID,
                json: true
              }, function(error, response, body) {
                expect(error).toBeNull();
                expect(body).not.toBeNull();
                expect(body.Code).toBe(30110, "This item should not have been found since it was deleted.");
                // console.log("Get Address Body: ", body);

                done();

              });
            });
          });
        });
      });
    });
  });
});