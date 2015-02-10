/** DocBarcode.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("DocBarcode.spec tests. | ", function() {
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

  /** Generate Barcode. */
  it("Validate Barcode Generator", function(done) {
    dataSource.createMsAccountScratch(request, {
      BarcodeID: 5001,
      BarcodeTypeId: 1,
      ForeignKey: 1,
      BarcodeNumber: 'SOSA001'

    }, function(accountValue) {
      // console.log("Account Value: ", accountValue);
      request.get({
          url: config.SseServicesCmsCORS + "DocBarcodeSrv/docBarcode/" + accountValue.AccountID + "/GenerateIndustryAccount",
          json: true
        },
        function(error, response, body) {
          // console.log("Body for generated Industry Account: ", body);
          expect(error).toBeNull();
          expect(body).not.toBeNull();
          expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);

          modelDef.industryAccountNumber(body.Value);
          done();
        });
    });
  });

  /** Get IndustryAccount numbers from an account Id. */
  it("Validate getting IndustryAccountNumbers", function(done) {
    dataSource.createMsAccountScratch(request, {
      BarcodeID: 5001,
      BarcodeTypeId: 1,
      ForeignKey: 1,
      BarcodeNumber: 'SOSA001'

    }, function(accountValue) {
      // console.log("Account Value: ", accountValue);
      dataSource.generateIndustryAccount(request, {
        accountID: accountValue.AccountID
      }, function() {
        request.get({
            url: config.SseServicesCmsCORS + "MonitoringStationSrv/MsAccounts/" + accountValue.AccountID + "/IndustryAccounts",
            json: true
          },
          function(error, response, body) {
            // console.log("Body: ", body);
            expect(error).toBeNull();
            expect(body).not.toBeNull();
            expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);

            /** Check that this is an array. */
            expect(body.Value.length).toBeGreaterThan(0, "There should at least be one item on the list.");
            body.Value.map(function(item) {
              modelDef.industryAccountNumber(item);
            });
            done();
          });
      });
    });
  });
});